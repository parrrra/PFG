using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using GymPlanner.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Exercise>> GetAllAsync(string? filter = null)
    {
        var query = _context.Exercises.Include(x => x.TrainingLevelProgressions).AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));

        return await query.Select(x => ToDomain(x)).ToListAsync();
    }

    public async Task<Exercise?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Exercises.Include(x => x.TrainingLevelProgressions).FirstOrDefaultAsync(x => x.Id == id);
        return entity is null ? null : ToDomain(entity);
    }

    public async Task CreateAsync(Exercise exercise)
    {
        var entity = ToEntity(exercise);
        _context.Exercises.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        var existingExercise = await _context.Exercises
            .Include(x => x.TrainingLevelProgressions)
            .FirstOrDefaultAsync(x => x.Id == exercise.Id);

        if (existingExercise is null) return;

        existingExercise.Name = exercise.Name;
        existingExercise.BodyPartId = exercise.BodyPartId;
        existingExercise.Description = exercise.Description;

        var progressionsToRemove = existingExercise.TrainingLevelProgressions
            .Where(existingProg => !exercise.TrainingLevelProgressions
                .Any(newProg => newProg.TrainingLevelId == existingProg.TrainingLevelId))
            .ToList();

        _context.ExerciseTrainingLevelProgressions.RemoveRange(progressionsToRemove);

        var progressionsToAdd = exercise.TrainingLevelProgressions
            .Where(newProg => !existingExercise.TrainingLevelProgressions
                .Any(existingProg => existingProg.TrainingLevelId == newProg.TrainingLevelId))
            .Select(newProg => new ExerciseTrainingLevelProgressionEntity
            {
                Id = Guid.NewGuid(),
                ExerciseId = existingExercise.Id,
                TrainingLevelId = newProg.TrainingLevelId,
                WeeklyImprovement = newProg.WeeklyImprovement
            })
            .ToList();

        await _context.ExerciseTrainingLevelProgressions.AddRangeAsync(progressionsToAdd);

        foreach (var existingProg in existingExercise.TrainingLevelProgressions)
        {
            var matchingNewProg = exercise.TrainingLevelProgressions
                .FirstOrDefault(newProg => newProg.TrainingLevelId == existingProg.TrainingLevelId);

            if (matchingNewProg != null)
            {
                existingProg.WeeklyImprovement = matchingNewProg.WeeklyImprovement;
            }
        }

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Exercises.FindAsync(id);
        if (entity is null) return;

        _context.Exercises.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync(string? filter = null)
    {
        var query = _context.Exercises.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));
        return await query.CountAsync();
    }

    public async Task<PaginatedResult<Exercise>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? bodyPartId = null)
    {
        var query = _context.Exercises.Include(x => x.TrainingLevelProgressions).AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));

        if (bodyPartId.HasValue)
            query = query.Where(x => x.BodyPartId == bodyPartId.Value);

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => ToDomain(x))
            .ToListAsync();

        return new PaginatedResult<Exercise>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    private static Exercise ToDomain(ExerciseEntity entity) =>
        new Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            BodyPartId = entity.BodyPartId,
            Description = entity.Description,
            TrainingLevelProgressions = entity.TrainingLevelProgressions.Select(tp => new ExerciseTrainingLevelProgression
            {
                TrainingLevelId = tp.TrainingLevelId,
                WeeklyImprovement = tp.WeeklyImprovement
            }).ToList()
        };

    private static ExerciseEntity ToEntity(Exercise exercise) =>
        new ExerciseEntity
        {
            Id = exercise.Id,
            Name = exercise.Name,
            BodyPartId = exercise.BodyPartId,
            Description = exercise.Description,
            TrainingLevelProgressions = exercise.TrainingLevelProgressions.Select(tp => new ExerciseTrainingLevelProgressionEntity
            {
                Id = Guid.NewGuid(),
                TrainingLevelId = tp.TrainingLevelId,
                WeeklyImprovement = tp.WeeklyImprovement
            }).ToList()
        };
}
