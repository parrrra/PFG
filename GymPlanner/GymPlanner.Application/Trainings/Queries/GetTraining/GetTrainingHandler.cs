using GymPlanner.Application.Trainings.DTOs;
using GymPlanner.Application.Trainings.Queries.GetTraining;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

public class GetTrainingHandler : IQueryHandler<GetTrainingQuery, TrainingDto?>
{
    private readonly ITrainingRepository _repository;

    public GetTrainingHandler(ITrainingRepository repo) => _repository = repo;

    public async Task<TrainingDto?> Handle(GetTrainingQuery query, CancellationToken cancellation)
    {
        var training = await _repository.GetByIdAsync(query.Id);
        if (training == null) return null;

        return new TrainingDto
        {
            Id = training.Id,
            UserId = training.UserId,
            TrainingTemplateId = training.TrainingTemplateId,
            TrainingTemplateName = training.TrainingTemplateName,
            RoutineId = training.RoutineId,
            Date = training.Date,
            IsCompleted = training.IsCompleted,
            Exercises = training.Exercises.OrderBy(x => x.Order).Select(e => new TrainingExerciseDto
            {
                Id = e.Id,
                TrainingId = e.TrainingId,
                ExerciseId = e.ExerciseId,
                ExerciseName = e.ExerciseName,
                Sets = e.Sets,
                Repetitions = e.Repetitions,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order,
                Notes = e.Notes,
                TrainingExerciseSets = e.TrainingExerciseSets
                    .OrderBy(s => s.Order)
                    .Select(s => new TrainingExerciseSetDto
                    {
                        Id = s.Id,
                        TrainingExerciseId = s.TrainingExerciseId,
                        Order = s.Order,
                        Repetitions = s.Repetitions,
                        Weight = s.Weight
                    }).ToList()
            }).ToList()
        };
    }
}
