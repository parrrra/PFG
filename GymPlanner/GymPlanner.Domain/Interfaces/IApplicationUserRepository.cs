using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetByIdAsync(Guid userId);
}