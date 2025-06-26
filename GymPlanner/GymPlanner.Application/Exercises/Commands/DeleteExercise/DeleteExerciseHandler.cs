using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Exercises.Commands.DeleteExercise;

public class DeleteExerciseHandler : ICommandHandler<DeleteExerciseCommand, bool>
{
    private readonly IExerciseRepository _repository;

    public DeleteExerciseHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteExerciseCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(command.Id);
        return true;
    }
}