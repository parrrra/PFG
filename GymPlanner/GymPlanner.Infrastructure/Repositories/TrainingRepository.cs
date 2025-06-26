using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly ApplicationDbContext _context;

    public TrainingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Training training)
    {
        var trainingEntity = new TrainingEntity
        {
            Id = training.Id,
            UserId = training.UserId,
            RoutineId = training.RoutineId,
            TrainingTemplateId = training.TrainingTemplateId,
            Date = training.Date,
            Exercises = training.Exercises.Select(te => new TrainingExerciseEntity
            {
                Id = te.Id,
                ExerciseId = te.ExerciseId,
                Sets = te.Sets,
                Repetitions = te.Repetitions,
                StartWeight = te.StartWeight,
                TargetWeight = te.TargetWeight,
                IsKeyExercise = te.IsKeyExercise,
                Order = te.Order,
                Notes = te.Notes,
                SetsDetails = te.TrainingExerciseSets.Select(set => new TrainingExerciseSetEntity
                {
                    Id = set.Id,
                    Order = set.Order,
                    Repetitions = set.Repetitions,
                    Weight = set.Weight
                }).ToList()
            }).ToList()
        };

        _context.Trainings.Add(trainingEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<Training?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Trainings
            .Include(t => t.TrainingTemplate)
            .Include(t => t.Exercises)
                .ThenInclude(e => e.Exercise)
            .Include(t => t.Exercises)
                .ThenInclude(e => e.SetsDetails)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (entity is null) return null;

        return new Training
        {
            Id = entity.Id,
            UserId = entity.UserId,
            RoutineId = entity.RoutineId,
            TrainingTemplateId = entity.TrainingTemplateId,
            TrainingTemplateName = entity.TrainingTemplate?.Name ?? string.Empty,
            Date = entity.Date,
            IsCompleted = entity.IsCompleted,
            Exercises = entity.Exercises.OrderBy(e => e.Order).Select(e => new TrainingExercise
            {
                Id = e.Id,
                TrainingId = entity.Id,
                ExerciseId = e.ExerciseId,
                ExerciseName = e.Exercise?.Name ?? string.Empty,
                Sets = e.Sets,
                Repetitions = e.Repetitions,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order,
                Notes = e.Notes,
                TrainingExerciseSets = e.SetsDetails?.OrderBy(s => s.Order).Select(s => new TrainingExerciseSet
                {
                    Id = s.Id,
                    TrainingExerciseId = s.TrainingExerciseId,
                    Order = s.Order,
                    Repetitions = s.Repetitions,
                    Weight = s.Weight
                }).ToList() ?? []
            }).ToList()
        };
    }

    public async Task<List<Training>> GetByUserAndMonthAsync(Guid userId, int year, int month)
    {
        var results = await _context.Trainings
            .Include(t => t.TrainingTemplate)
            .Include(t => t.Exercises)
                .ThenInclude(e => e.Exercise)
            .Where(t => t.UserId == userId.ToString() &&
                        t.Date.Year == year &&
                        t.Date.Month == month)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        return results.Select(t => new Training
        {
            Id = t.Id,
            UserId = t.UserId,
            RoutineId = t.RoutineId,
            TrainingTemplateId = t.TrainingTemplateId,
            TrainingTemplateName = t.TrainingTemplate?.Name ?? string.Empty,
            Date = t.Date,
            Exercises = t.Exercises.Select(e => new TrainingExercise
            {
                Id = e.Id,
                ExerciseId = e.ExerciseId,
                ExerciseName = e.Exercise?.Name ?? string.Empty,
                Repetitions = e.Repetitions,
                Sets = e.Sets,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order,
                Notes = e.Notes,
                TrainingExerciseSets = e.SetsDetails?.OrderBy(s => s.Order).Select(s => new TrainingExerciseSet
                {
                    Id = s.Id,
                    TrainingExerciseId = s.TrainingExerciseId,
                    Order = s.Order,
                    Repetitions = s.Repetitions,
                    Weight = s.Weight
                }).ToList() ?? []
            }).ToList()
        }).ToList();
    }

    public async Task UpdateAsync(Training training)
    {
        var entity = await _context.Trainings
            .Include(t => t.Exercises)
                .ThenInclude(e => e.SetsDetails)
            .FirstOrDefaultAsync(t => t.Id == training.Id);

        if (entity is null) return;

        entity.RoutineId = training.RoutineId;
        entity.TrainingTemplateId = training.TrainingTemplateId;
        entity.Date = training.Date;
        entity.IsCompleted = training.IsCompleted;

        var exercisesToRemove = entity.Exercises
            .Where(e => !training.Exercises.Any(te => te.Id == e.Id))
            .ToList();
        _context.TrainingExercises.RemoveRange(exercisesToRemove);

        var exercisesToAdd = training.Exercises
            .Where(te => !entity.Exercises.Any(e => e.Id == te.Id))
            .Select(te => new TrainingExerciseEntity
            {
                Id = te.Id == Guid.Empty ? Guid.NewGuid() : te.Id,
                TrainingId = training.Id,
                ExerciseId = te.ExerciseId,
                Sets = te.Sets,
                Repetitions = te.Repetitions,
                StartWeight = te.StartWeight,
                TargetWeight = te.TargetWeight,
                IsKeyExercise = te.IsKeyExercise,
                Order = te.Order,
                Notes = te.Notes,
                SetsDetails = te.TrainingExerciseSets.Select(set => new TrainingExerciseSetEntity
                {
                    Id = set.Id == Guid.Empty ? Guid.NewGuid() : set.Id,
                    TrainingExerciseId = te.Id,
                    Order = set.Order,
                    Repetitions = set.Repetitions,
                    Weight = set.Weight
                }).ToList()
            }).ToList();

        await _context.TrainingExercises.AddRangeAsync(exercisesToAdd);

        foreach (var exEntity in entity.Exercises)
        {
            var exModel = training.Exercises.FirstOrDefault(x => x.Id == exEntity.Id);
            if (exModel is null) continue;

            exEntity.Sets = exModel.Sets;
            exEntity.Repetitions = exModel.Repetitions;
            exEntity.StartWeight = exModel.StartWeight;
            exEntity.TargetWeight = exModel.TargetWeight;
            exEntity.IsKeyExercise = exModel.IsKeyExercise;
            exEntity.Order = exModel.Order;
            exEntity.Notes = exModel.Notes;

            var setsToRemove = exEntity.SetsDetails
                .Where(s => !exModel.TrainingExerciseSets.Any(sm => sm.Id == s.Id))
                .ToList();
            _context.TrainingExerciseSets.RemoveRange(setsToRemove);

            var setsToAdd = exModel.TrainingExerciseSets
                .Where(sm => !exEntity.SetsDetails.Any(s => s.Id == sm.Id))
                .Select(sm => new TrainingExerciseSetEntity
                {
                    Id = sm.Id == Guid.Empty ? Guid.NewGuid() : sm.Id,
                    TrainingExerciseId = exEntity.Id,
                    Order = sm.Order,
                    Repetitions = sm.Repetitions,
                    Weight = sm.Weight
                }).ToList();

            await _context.TrainingExerciseSets.AddRangeAsync(setsToAdd);

            foreach (var sEntity in exEntity.SetsDetails)
            {
                var sModel = exModel.TrainingExerciseSets.FirstOrDefault(x => x.Id == sEntity.Id);
                if (sModel is null) continue;
                sEntity.Order = sModel.Order;
                sEntity.Repetitions = sModel.Repetitions;
                sEntity.Weight = sModel.Weight;
            }
        }

        await _context.SaveChangesAsync();
    }


    public async Task DeleteFromDateAsync(Guid routineId, DateOnly fromDate)
    {
        var trainingsToDelete = await _context.Trainings
            .Where(t => !t.IsCompleted)
            .Where(t => t.Date >= fromDate)
            .Where(t => t.RoutineId == routineId)
            .Include(t => t.Exercises)
                .ThenInclude(e => e.SetsDetails)
            .ToListAsync();

        _context.Trainings.RemoveRange(trainingsToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Training>> GetByUserAndDateRangeAsync(Guid userId, DateOnly? startDate, DateOnly? endDate, Guid? exerciseId = null)
    {
        var query = _context.Trainings
            .AsNoTracking()
            .AsSplitQuery()
            .Where(t => t.UserId == userId.ToString());

        if (startDate.HasValue)
            query = query.Where(t => t.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.Date <= endDate.Value);

        var results = await query
            .Select(t => new Training
            {
                Id = t.Id,
                UserId = t.UserId,
                RoutineId = t.RoutineId,
                TrainingTemplateId = t.TrainingTemplateId,
                TrainingTemplateName = t.TrainingTemplate != null ? t.TrainingTemplate.Name : string.Empty,
                Date = t.Date,
                IsCompleted = t.IsCompleted,
                Exercises = t.Exercises
                    .Where(e => !exerciseId.HasValue || e.ExerciseId == exerciseId.Value)
                    .Select(e => new TrainingExercise
                    {
                        Id = e.Id,
                        ExerciseId = e.ExerciseId,
                        ExerciseName = e.Exercise != null ? e.Exercise.Name : string.Empty,
                        Repetitions = e.Repetitions,
                        Sets = e.Sets,
                        StartWeight = e.StartWeight,
                        TargetWeight = e.TargetWeight,
                        IsKeyExercise = e.IsKeyExercise,
                        Order = e.Order,
                        Notes = e.Notes,
                        TrainingExerciseSets = e.SetsDetails
                            .OrderBy(s => s.Order)
                            .Select(s => new TrainingExerciseSet
                            {
                                Id = s.Id,
                                TrainingExerciseId = s.TrainingExerciseId,
                                Order = s.Order,
                                Repetitions = s.Repetitions,
                                Weight = s.Weight
                            }).ToList()
                    }).ToList()
            })
            .ToListAsync();

        return results;
    }


}
