using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories
{
    public class TrainingLevelRepository : ITrainingLevelRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingLevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingLevel>> GetAllAsync()
        {
            return await _context.TrainingLevels
                .OrderBy(e => e.Order)
                .Select(e => new TrainingLevel
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToListAsync();
        }
    }
}