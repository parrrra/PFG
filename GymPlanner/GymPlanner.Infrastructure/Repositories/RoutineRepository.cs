using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly ApplicationDbContext _context;
        public RoutineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Routine>> GetAllAsync(string? filter = null, Guid? userId = null)
        {
            var query = _context.Routines
                .Include(x => x.Days)
                    .ThenInclude(d => d.Trainings)
                        .ThenInclude(t => t.TrainingTemplate)
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));
            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.ToString());

            return await query
                .OrderBy(x => x.Name)
                .Select(x => ToDomain(x))
                .ToListAsync();
        }

        public async Task<Routine?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Routines
                .Include(x => x.Days)
                    .ThenInclude(d => d.Trainings)
                        .ThenInclude(t => t.TrainingTemplate)
                            .ThenInclude(t => t.Exercises)
                                .ThenInclude(e => e.Exercise)
                                    .ThenInclude(e => e.TrainingLevelProgressions)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity is null ? null : ToDomain(entity);
        }

        public async Task CreateAsync(Routine routine)
        {
            var entity = ToEntity(routine);
            _context.Routines.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Routine routine)
        {
            var existing = await _context.Routines
                .Include(x => x.Days)
                    .ThenInclude(d => d.Trainings)
                .FirstOrDefaultAsync(x => x.Id == routine.Id);

            if (existing is null) return;

            existing.Name = routine.Name;
            existing.Description = routine.Description;
            existing.IsActive = routine.IsActive;
            existing.UserId = routine.UserId.ToString();
            await _context.SaveChangesAsync();

            var daysToRemove = existing.Days
                .Where(d => !routine.Days.Any(rd => rd.Id == d.Id))
                .ToList();
            _context.RoutineDays.RemoveRange(daysToRemove);

            var daysToAdd = routine.Days
                .Where(rd => !existing.Days.Any(d => d.Id == rd.Id))
                .Select(ToEntity)
                .ToList();
            await _context.RoutineDays.AddRangeAsync(daysToAdd);

            foreach (var day in existing.Days)
            {
                var updatedDay = routine.Days.FirstOrDefault(rd => rd.Id == day.Id);
                if (updatedDay != null)
                {
                    day.DayOfWeek = updatedDay.DayOfWeek;

                    var trainingsToRemove = day.Trainings
                        .Where(t => !updatedDay.TrainingTemplates.Any(ut => ut.Id == t.Id))
                        .ToList();
                    _context.RoutineDayTrainingTemplates.RemoveRange(trainingsToRemove);

                    var trainingsToAdd = updatedDay.TrainingTemplates
                        .Where(ut => !day.Trainings.Any(t => t.Id == ut.Id))
                        .ToList();

                    await _context.RoutineDayTrainingTemplates.AddRangeAsync(
                        trainingsToAdd.Select(t => new RoutineDayTrainingTemplateEntity
                        {
                            Id = Guid.NewGuid(),
                            TrainingTemplateId = t.Id,
                            RoutineDayId = day.Id
                        }));
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Routines.FindAsync(id);
            if (entity is null) return;
            _context.Routines.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(string? filter = null, Guid? userId = null)
        {
            var query = _context.Routines.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));
            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.ToString());
            return await query.CountAsync();
        }

        public async Task<PaginatedResult<Routine>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? userId = null)
        {
            var query = _context.Routines
                .Include(x => x.Days)
                    .ThenInclude(d => d.Trainings)
                        .ThenInclude(t => t.TrainingTemplate)
                .AsNoTracking().AsQueryable();

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

            return new PaginatedResult<Routine>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        private static Routine ToDomain(RoutineEntity entity) => new Routine
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            UserId = new Guid(entity.UserId ?? Guid.Empty.ToString()),
            IsActive = entity.IsActive,
            Days = entity.Days?.OrderBy(x => x.DayOfWeek).Select(ToDomain).ToList() ?? []
        };

        private static RoutineDay ToDomain(RoutineDayEntity entity) => new RoutineDay
        {
            Id = entity.Id,
            DayOfWeek = entity.DayOfWeek,
            TrainingTemplates = entity.Trainings?
                .Where(t => t.TrainingTemplate != null)
                .Select(t => ToDomain(t.TrainingTemplate!))
                .ToList() ?? []
        };

        public static TrainingTemplate ToDomain(TrainingTemplateEntity entity) =>
            new TrainingTemplate
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                UserId = new Guid(entity.UserId ?? Guid.Empty.ToString()),
                Exercises = entity.Exercises?
                    .Select(e => ToDomain(e)).ToList() ?? []
            };

        private static TrainingTemplateExercise ToDomain(TrainingTemplateExerciseEntity e)
        {
            return new TrainingTemplateExercise
            {
                Id = e.Id,
                ExerciseId = e.ExerciseId,
                Sets = e.Sets,
                Repetitions = e.Repetitions,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order,
                Exercise = new Exercise
                {
                    Id = e.Exercise.Id,
                    Name = e.Exercise.Name,
                    BodyPartId = e.Exercise.BodyPartId,
                    TrainingLevelProgressions = e.Exercise.TrainingLevelProgressions
                        .Select(p => new ExerciseTrainingLevelProgression
                        {
                            TrainingLevelId = p.TrainingLevelId,
                            WeeklyImprovement = p.WeeklyImprovement
                        }).ToList()
                }
            };
        }

        private static RoutineEntity ToEntity(Routine domain)
        {
            var entity = new RoutineEntity
            {
                Id = domain.Id == Guid.Empty ? Guid.NewGuid() : domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                UserId = domain.UserId.ToString(),
                IsActive = domain.IsActive,
                Days = domain.Days.Select(ToEntity).ToList()
            };
            foreach (var day in entity.Days)
                day.RoutineId = entity.Id;
            return entity;
        }

        private static RoutineDayEntity ToEntity(RoutineDay domain)
        {
            var entity = new RoutineDayEntity
            {
                Id = domain.Id == Guid.Empty ? Guid.NewGuid() : domain.Id,
                DayOfWeek = domain.DayOfWeek,
                Trainings = domain.TrainingTemplates.Select(t => new RoutineDayTrainingTemplateEntity
                {
                    Id = Guid.NewGuid(),
                    TrainingTemplateId = t.Id,
                }).ToList()
            };
            return entity;
        }
    }
}
