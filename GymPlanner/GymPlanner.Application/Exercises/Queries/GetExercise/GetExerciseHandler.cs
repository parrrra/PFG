using GymPlanner.Application.Exercises.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Exercises.Queries.GetExercise;

public class GetExerciseHandler : IQueryHandler<GetExerciseQuery, ExerciseDto?>
{
    private readonly IExerciseRepository _repository;

    public GetExerciseHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExerciseDto?> Handle(GetExerciseQuery query, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetByIdAsync(query.Id);
        if (exercise is null) return null;

        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            BodyPartId = exercise.BodyPartId,
            Description = exercise.Description,
            TrainingLevelProgressions = exercise.TrainingLevelProgressions.Select(x => new ExerciseProgressionDto
            {
                TrainingLevelId = x.TrainingLevelId,
                TrainingLevelName = string.Empty,
                WeeklyImprovement = x.WeeklyImprovement
            }).ToList()
        };
    }
}