using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetByIdAsync(Guid userId)
    {
        var entity = await _context.Users
            .Include(u => u.TrainingLevel)
            .FirstOrDefaultAsync(u => u.Id == userId.ToString());

        if (entity is null) return null;

        return new ApplicationUser
        {
            Id = new Guid(entity.Id),
            TrainingLevelId = entity.TrainingLevelId,
            FullName = entity.FullName,
        };
    }
}
