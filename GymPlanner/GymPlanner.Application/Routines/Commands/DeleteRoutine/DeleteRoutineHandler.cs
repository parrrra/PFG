using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Commands.DeleteRoutine;

public class DeleteRoutineHandler : ICommandHandler<DeleteRoutineCommand, bool>
{
    private readonly IRoutineRepository _repository;

    public DeleteRoutineHandler(IRoutineRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteRoutineCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(command.Id);
        return true;
    }
}
