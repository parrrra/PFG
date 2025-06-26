using GymPlanner.Application.Routines.Commands.ActivateRoutine;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Commands.DeactivateRoutine;

public class DeactivateRoutineHandler : ICommandHandler<DeactivateRoutineCommand, bool>
{
    private readonly IRoutineService _routineService;

    public DeactivateRoutineHandler(IRoutineService routineService)
    {
        _routineService = routineService;
    }

    public async Task<bool> Handle(DeactivateRoutineCommand command, CancellationToken cancellation)
    {
        await _routineService.DeactivateRoutineAsync(command.Id);
        return true;
    }
}
