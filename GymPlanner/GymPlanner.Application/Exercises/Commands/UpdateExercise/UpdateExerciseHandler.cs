using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseHandler : ICommandHandler<UpdateExerciseCommand, bool>
{
    private readonly IExerciseRepository _repository;

    public UpdateExerciseHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        var exercise = new Exercise
        {
            Id = command.Id,
            Name = command.Name,
            BodyPartId = command.BodyPartId,
            Description = command.Description,
            TrainingLevelProgressions = command.TrainingLevelProgressions.Select(tp => new ExerciseTrainingLevelProgression
            {
                TrainingLevelId = tp.TrainingLevelId,
                WeeklyImprovement = tp.WeeklyImprovement
            }).ToList()
        };

        await _repository.UpdateAsync(exercise);
        return true;
    }
}