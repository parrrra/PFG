using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using GymPlanner.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories;

public class TrainingTemplateRepository : ITrainingTemplateRepository
{
    private readonly ApplicationDbContext _context;

    public TrainingTemplateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrainingTemplate>> GetAllAsync(string? filter = null, Guid? userId = null)
    {
        var query = _context.TrainingTemplates
            .Include(x => x.Exercises)
                .ThenInclude(te => te.Exercise)
                    .ThenInclude(e => e.BodyPart)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));
        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.ToString());

        return await query
            .OrderBy(x => x.Name)
            .Select(x => ToDomain(x))
            .ToListAsync();
    }

    public async Task<TrainingTemplate?> GetByIdAsync(Guid id)
    {
        var entity = await _context.TrainingTemplates
            .Include(x => x.Exercises)
                .ThenInclude(te => te.Exercise)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity is null ? null : ToDomain(entity);
    }

    public async Task CreateAsync(TrainingTemplate template)
    {
        var entity = ToEntity(template);

        _context.TrainingTemplates.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TrainingTemplate template)
    {
        var existing = await _context.TrainingTemplates
            .Include(x => x.Exercises)
            .FirstOrDefaultAsync(x => x.Id == template.Id);

        if (existing is null) return;

        existing.Name = template.Name;
        existing.Description = template.Description;
        existing.UserId = template.UserId.ToString();

        var exercisesToRemove = existing.Exercises
            .Where(e => !template.Exercises.Any(te => te.Id == e.Id))
            .ToList();
        _context.TrainingTemplateExercises.RemoveRange(exercisesToRemove);

        var exercisesToAdd = template.Exercises
            .Where(te => !existing.Exercises.Any(e => e.Id == te.Id))
            .Select(te => ToEntity(te))
            .ToList();

        await _context.TrainingTemplateExercises.AddRangeAsync(exercisesToAdd);

        foreach (var existingEx in existing.Exercises)
        {
            var updated = template.Exercises.FirstOrDefault(te => te.Id == existingEx.Id);
            if (updated is not null)
            {
                existingEx.ExerciseId = updated.ExerciseId;
                existingEx.Sets = updated.Sets;
                existingEx.Repetitions = updated.Repetitions;
                existingEx.StartWeight = updated.StartWeight;
                existingEx.TargetWeight = updated.TargetWeight;
                existingEx.IsKeyExercise = updated.IsKeyExercise;
                existingEx.Order = updated.Order;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.TrainingTemplates.FindAsync(id);
        if (entity is null) return;
        _context.TrainingTemplates.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync(string? filter = null, Guid? userId = null)
    {
        var query = _context.TrainingTemplates.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));
        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.ToString());
        return await query.CountAsync();
    }

    public async Task<PaginatedResult<TrainingTemplate>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? userId = null)
    {
        var query = _context.TrainingTemplates
            .Include(x => x.Exercises)
                .ThenInclude(te => te.Exercise)
                    .ThenInclude(e => e.BodyPart)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(x => x.Name.Contains(filter));
        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.ToString());

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => ToDomain(x))
            .ToListAsync();

        return new PaginatedResult<TrainingTemplate>
        {
            Items = items,
            TotalCount = totalCount
        };
    }



    private static TrainingTemplate ToDomain(TrainingTemplateEntity entity) =>
        new TrainingTemplate
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            UserId = new Guid(entity.UserId ?? Guid.Empty.ToString()),
            Exercises = entity.Exercises?
                .OrderBy(x => x.Order)
                .Select(ToDomain).ToList() ?? []
        };

    private static TrainingTemplateExercise ToDomain(TrainingTemplateExerciseEntity entity) =>
        new TrainingTemplateExercise
        {
            Id = entity.Id,
            TrainingTemplateId = entity.TrainingTemplateId,
            ExerciseId = entity.ExerciseId,
            ExerciseName = entity.Exercise?.Name ?? string.Empty,
            BodyPartId = entity.Exercise?.BodyPartId ?? Guid.Empty,
            BodyPartName = entity.Exercise?.BodyPart?.Name ?? string.Empty,
            Sets = entity.Sets,
            Repetitions = entity.Repetitions,
            StartWeight = entity.StartWeight,
            TargetWeight = entity.TargetWeight,
            IsKeyExercise = entity.IsKeyExercise,
            Order = entity.Order
        };

    private static TrainingTemplateEntity ToEntity(TrainingTemplate domain)
    {
        var entity = new TrainingTemplateEntity
        {
            Id = domain.Id == Guid.Empty ? Guid.NewGuid() : domain.Id,
            Name = domain.Name,
            Description = domain.Description,
            UserId = domain.UserId.ToString(),
            Exercises = domain.Exercises?.Select(ToEntity).ToList() ?? []
        };

        foreach (var ex in entity.Exercises)
        {
            ex.TrainingTemplateId = entity.Id;
        }
        return entity;
    }

    private static TrainingTemplateExerciseEntity ToEntity(TrainingTemplateExercise domain) =>
        new TrainingTemplateExerciseEntity
        {
            Id = domain.Id == Guid.Empty ? Guid.NewGuid() : domain.Id,
            TrainingTemplateId = domain.TrainingTemplateId,
            ExerciseId = domain.ExerciseId,
            Sets = domain.Sets,
            Repetitions = domain.Repetitions,
            StartWeight = domain.StartWeight,
            TargetWeight = domain.TargetWeight,
            IsKeyExercise = domain.IsKeyExercise,
            Order = domain.Order
        };
}
