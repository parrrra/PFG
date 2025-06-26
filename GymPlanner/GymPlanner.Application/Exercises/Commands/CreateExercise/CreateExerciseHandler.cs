using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Common.CQRS;

namespace GymPlanner.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseHandler : ICommandHandler<CreateExerciseCommand, Guid>
{
    private readonly IExerciseRepository _repository;

    public CreateExerciseHandler(IExerciseRepository repo)
    {
        _repository = repo;
    }

    public async Task<Guid> Handle(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        var exercise = new Exercise
        {
            Name = command.Name,
            BodyPartId = command.BodyPartId,
            Description = command.Description,
            TrainingLevelProgressions = command.TrainingLevelProgressions.Select(tp => new ExerciseTrainingLevelProgression
            {
                TrainingLevelId = tp.TrainingLevelId,
                WeeklyImprovement = tp.WeeklyImprovement
            }).ToList()
        };

        await _repository.CreateAsync(exercise);
        return exercise.Id;
    }
}
