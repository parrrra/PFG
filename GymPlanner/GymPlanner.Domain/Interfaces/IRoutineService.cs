namespace GymPlanner.Domain.Interfaces;

public interface IRoutineService
{
    Task ActivateExclusiveRoutineAsync(Guid routineId, Guid userId);

    Task DeactivateRoutineAsync(Guid routineId);
}