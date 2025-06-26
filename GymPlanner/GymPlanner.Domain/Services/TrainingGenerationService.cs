using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Domain.Services;

public class TrainingGenerationService : ITrainingGenerationService
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IApplicationUserRepository _userRepository;
    private readonly ITrainingRepository _trainingRepository;

    public TrainingGenerationService(
        IRoutineRepository routineRepository,
        IApplicationUserRepository userRepository,
        ITrainingRepository trainingRepository)
    {
        _routineRepository = routineRepository;
        _userRepository = userRepository;
        _trainingRepository = trainingRepository;
    }


    public async Task GenerateTrainingsAsync(Guid routineId, Guid userId)
    {
        var routine = await _routineRepository.GetByIdAsync(routineId);
        if (routine is null) return;

        var user = await _userRepository.GetByIdAsync(userId);
        var trainingLevelId = user.TrainingLevelId;

        var progressionMap = GetProgressionMap(routine, trainingLevelId, out var weeksRequiredPerExercise);

        var maxWeeks = weeksRequiredPerExercise.Values.DefaultIfEmpty(1).Max();
        var today = DateOnly.FromDateTime(DateTime.Today);
        var routineDays = routine.Days.OrderBy(d => d.DayOfWeek).ToList();

        for (int week = 0; week <= maxWeeks; week++)
        {
            foreach (var day in routineDays)
            {
                int daysUntilFirstMonday = (8 - (int)DateTime.Today.DayOfWeek) % 7;
                var baseMonday = DateOnly.FromDateTime(DateTime.Today.AddDays(daysUntilFirstMonday));

                var currentDate = baseMonday.AddDays((day.DayOfWeek - 1) + (week * 7));

                foreach (var template in day.TrainingTemplates)
                {
                    var training = CreateTrainingInstance(userId, routine.Id, template.Id, currentDate);

                    foreach (var exercise in template.Exercises)
                    {
                        var trainingExercise = CreateTrainingExercise(exercise, week, weeksRequiredPerExercise, progressionMap);
                        training.Exercises.Add(trainingExercise);
                    }

                    await _trainingRepository.CreateAsync(training);
                }
            }
        }

    }

    private Dictionary<Guid, double> GetProgressionMap(Routine routine, Guid trainingLevelId, out Dictionary<Guid, int> weeksRequiredPerExercise)
    {
        var keyExercises = routine.Days
            .SelectMany(d => d.TrainingTemplates)
            .SelectMany(t => t.Exercises)
            .DistinctBy(e => e.Id)
            .Where(e => e.IsKeyExercise && e.TargetWeight.HasValue)
            .ToList();

        var progressionMap = keyExercises
            .Select(e => new
            {
                e.ExerciseId,
                WeeklyImprovement = e.Exercise.TrainingLevelProgressions
                    .FirstOrDefault(p => p.TrainingLevelId == trainingLevelId)?.WeeklyImprovement ?? 0
            })
            .ToDictionary(x => x.ExerciseId, x => x.WeeklyImprovement);

        weeksRequiredPerExercise = keyExercises.ToDictionary(
            e => e.Id,
            e =>
            {
                var improvement = progressionMap.TryGetValue(e.ExerciseId, out var val) ? val : 0;
                return CalculateWeeksToTarget(e.StartWeight, e.TargetWeight!.Value, improvement);
            });

        return progressionMap;
    }

    private Training CreateTrainingInstance(Guid userId, Guid routineId, Guid trainingTemplateId, DateOnly date) => new()
    {
        Id = Guid.NewGuid(),
        UserId = userId.ToString(),
        RoutineId = routineId,
        TrainingTemplateId = trainingTemplateId,
        Date = date,
        Exercises = []
    };

    private TrainingExercise CreateTrainingExercise(
        TrainingTemplateExercise exercise,
        int week,
        Dictionary<Guid, int> weeksRequiredPerExercise,
        Dictionary<Guid, double> progressionMap)
    {
        double weight;

        if (exercise.IsKeyExercise && weeksRequiredPerExercise.ContainsKey(exercise.Id))
        {
            var prog = progressionMap[exercise.ExerciseId];
            weight = exercise.StartWeight + prog * week;
            weight = Math.Min(weight, exercise.TargetWeight!.Value);
        }
        else
        {
            weight = exercise.StartWeight;
        }

        var sets = Enumerable.Range(1, exercise.Sets).Select(s => new TrainingExerciseSet
        {
            Id = Guid.NewGuid(),
            Order = s,
            Repetitions = exercise.Repetitions,
            Weight = weight
        }).ToList();

        return new TrainingExercise
        {
            Id = Guid.NewGuid(),
            ExerciseId = exercise.ExerciseId,
            Sets = exercise.Sets,
            Repetitions = exercise.Repetitions,
            IsKeyExercise = exercise.IsKeyExercise,
            StartWeight = weight,
            TargetWeight = exercise.TargetWeight,
            TrainingExerciseSets = sets,
            Order = exercise.Order,
        };
    }

    private int CalculateWeeksToTarget(double startWeight, double targetWeight, double weeklyImprovement)
    {
        if (weeklyImprovement <= 0 || targetWeight <= startWeight)
            return 1;

        return (int)Math.Ceiling((targetWeight - startWeight) / weeklyImprovement);
    }
}
