using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class BodyPartRepository : IBodyPartRepository
    {
        private readonly ApplicationDbContext _context;

        public BodyPartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BodyPart>> GetAllAsync(string? filter = null)
        {
            var query = _context.BodyParts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));

            return await query
                .Select(x => new BodyPart(x.Id, x.Name))
                .ToListAsync();
        }

        public async Task<BodyPart?> GetByIdAsync(Guid id)
        {
            var entity = await _context.BodyParts.FindAsync(id);
            return entity is null ? null : new BodyPart(entity.Id, entity.Name);
        }

        public async Task CreateAsync(BodyPart bodyPart)
        {
            var entity = new Persistence.Entities.BodyPartEntity(bodyPart.Id, bodyPart.Name);
            _context.BodyParts.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BodyPart bodyPart)
        {
            var entity = await _context.BodyParts.FindAsync(bodyPart.Id);
            if (entity is null) return;

            entity.Name = bodyPart.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.BodyParts.FindAsync(id);
            if (entity is null) return;

            _context.BodyParts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(string? filter = null)
        {
            var query = _context.BodyParts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));

            return await query.CountAsync();
        }

        public async Task<PaginatedResult<BodyPart>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null)
        {
            var query = _context.BodyParts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new BodyPart(x.Id, x.Name))
                .ToListAsync();

            return new PaginatedResult<BodyPart>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
