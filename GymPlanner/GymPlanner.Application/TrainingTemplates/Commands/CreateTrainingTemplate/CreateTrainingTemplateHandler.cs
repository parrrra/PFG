using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingTemplates.Commands.CreateTrainingTemplate;

public class CreateTrainingTemplateHandler : ICommandHandler<CreateTrainingTemplateCommand, Guid>
{
    private readonly ITrainingTemplateRepository _repository;

    public CreateTrainingTemplateHandler(ITrainingTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTrainingTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = new TrainingTemplate
        {
            Name = command.Name,
            Description = command.Description,
            UserId = command.UserId,
            Exercises = command.Exercises
                .Select(e => new TrainingTemplateExercise
                {
                    ExerciseId = e.ExerciseId,
                    Sets = e.Sets,
                    Repetitions = e.Repetitions,
                    StartWeight = e.StartWeight,
                    TargetWeight = e.TargetWeight,
                    IsKeyExercise = e.IsKeyExercise,
                    Order = e.Order
                }).ToList()
        };

        await _repository.CreateAsync(template);
        return template.Id;
    }
}
