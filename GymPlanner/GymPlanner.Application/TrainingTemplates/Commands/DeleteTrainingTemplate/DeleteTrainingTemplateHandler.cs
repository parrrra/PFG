using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingTemplates.Commands.DeleteTrainingTemplate;

public class DeleteTrainingTemplateHandler : ICommandHandler<DeleteTrainingTemplateCommand, bool>
{
    private readonly ITrainingTemplateRepository _repository;

    public DeleteTrainingTemplateHandler(ITrainingTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTrainingTemplateCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(command.Id);
        return true;
    }
}
