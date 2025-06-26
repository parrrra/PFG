using GymPlanner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure.Services;

public class RoutineDataService : IRoutineDataService
{
    private readonly IRoutineRepository _repository;

    private readonly ApplicationDbContext _context;


    public RoutineDataService(IRoutineRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task DeactivateAllAsync(Guid userId)
    {
        var routines = _context.Routines
            .Where(r => r.UserId == userId.ToString() && r.IsActive);

        await routines.ExecuteUpdateAsync(r => r
            .SetProperty(r => r.IsActive, false));
    }

    public async Task ActivateAsync(Guid routineId)
    {
        await SetRoutineActiveStateAsync(routineId, true);
    }

    public async Task DeactivateAsync(Guid routineId)
    {
        await SetRoutineActiveStateAsync(routineId, false);
    }

    private async Task SetRoutineActiveStateAsync(Guid routineId, bool isActive)
    {
        var routine = await _repository.GetByIdAsync(routineId);

        if (routine is null) return;

        await _context.Routines
            .Where(r => r.Id == routineId)
            .ExecuteUpdateAsync(r => r
                .SetProperty(r => r.IsActive, isActive));
    }
}