using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Trainings.Commands.UpdateTraining;

public class UpdateTrainingHandler : ICommandHandler<UpdateTrainingCommand, bool>
{
    private readonly ITrainingRepository _repository;

    public UpdateTrainingHandler(ITrainingRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateTrainingCommand command, CancellationToken cancellation)
    {
        var training = await _repository.GetByIdAsync(command.Id);
        if (training is null) return false;

        training.IsCompleted = command.IsCompleted;

        training.Exercises = command.Exercises
            .Select(exercise => new TrainingExercise
            {
                Id = exercise.Id,
                TrainingId = command.Id,
                Sets = exercise.Sets,
                Repetitions = exercise.Repetitions,
                StartWeight = exercise.StartWeight,
                TargetWeight = exercise.TargetWeight,
                IsKeyExercise = exercise.IsKeyExercise,
                Order = exercise.Order,
                Notes = exercise.Notes,
                TrainingExerciseSets = exercise.TrainingExerciseSets
                    .Select(set => new TrainingExerciseSet
                    {
                        Id = set.Id,
                        TrainingExerciseId = exercise.Id,
                        Order = set.Order,
                        Repetitions = set.Repetitions,
                        Weight = set.Weight
                    }).ToList()
            })
            .ToList();

        await _repository.UpdateAsync(training);
        return true;
    }

}
