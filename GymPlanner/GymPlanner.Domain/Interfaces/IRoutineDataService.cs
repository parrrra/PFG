namespace GymPlanner.Domain.Interfaces;

public interface IRoutineDataService
{
    Task DeactivateAllAsync(Guid userId);
    Task ActivateAsync(Guid routineId);
    Task DeactivateAsync(Guid routineId);
}