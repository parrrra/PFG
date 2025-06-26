using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingTemplates.Commands.UpdateTrainingTemplate;

public class UpdateTrainingTemplateHandler : ICommandHandler<UpdateTrainingTemplateCommand, bool>
{
    private readonly ITrainingTemplateRepository _repository;

    public UpdateTrainingTemplateHandler(ITrainingTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTrainingTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = new TrainingTemplate
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            UserId = command.UserId,
            Exercises = command.Exercises.Select(e => new TrainingTemplateExercise
            {
                Id = e.Id,
                TrainingTemplateId = command.Id,
                ExerciseId = e.ExerciseId,
                Sets = e.Sets,
                Repetitions = e.Repetitions,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order
            }).ToList()
        };

        await _repository.UpdateAsync(template);
        return true;
    }
}
