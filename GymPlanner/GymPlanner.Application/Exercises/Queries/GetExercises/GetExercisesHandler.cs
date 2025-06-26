using GymPlanner.Application.Exercises.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Common.Utils;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Exercises.Queries.GetExercises;

public class GetExercisesHandler : IQueryHandler<GetExercisesQuery, PaginatedResult<ExerciseDto>>
{
    private readonly IExerciseRepository _repository;

    public GetExercisesHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<ExerciseDto>> Handle(GetExercisesQuery query, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPaginatedAsync(query.PageNumber, query.PageSize, query.Filter, query.BodyPartId);

        return new PaginatedResult<ExerciseDto>
        {
            Items = result.Items.Select(x => new ExerciseDto
            {
                Id = x.Id,
                Name = x.Name,
                BodyPartId = x.BodyPartId,
                Description = x.Description,
                TrainingLevelProgressions = x.TrainingLevelProgressions.Select(tp => new ExerciseProgressionDto
                {
                    TrainingLevelId = tp.TrainingLevelId,
                    TrainingLevelName = string.Empty,
                    WeeklyImprovement = tp.WeeklyImprovement
                }).ToList()
            }).ToList(),
            TotalCount = result.TotalCount
        };
    }
}