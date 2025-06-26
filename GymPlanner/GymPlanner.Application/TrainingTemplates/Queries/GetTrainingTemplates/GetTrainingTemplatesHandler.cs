using GymPlanner.Application.TrainingTemplates.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Common.Utils;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingTemplates.Queries.GetTrainingTemplates;

public class GetTrainingTemplatesHandler : IQueryHandler<GetTrainingTemplatesQuery, PaginatedResult<TrainingTemplateDto>>
{
    private readonly ITrainingTemplateRepository _repository;

    public GetTrainingTemplatesHandler(ITrainingTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<TrainingTemplateDto>> Handle(GetTrainingTemplatesQuery query, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPaginatedAsync(query.PageNumber, query.PageSize, query.Filter, query.UserId);

        return new PaginatedResult<TrainingTemplateDto>
        {
            Items = result.Items.Select(t => new TrainingTemplateDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                UserId = t.UserId,
                Exercises = t.Exercises
                    .Select(e => new TrainingTemplateExerciseDto
                    {
                        Id = e.Id,
                        ExerciseId = e.ExerciseId,
                        ExerciseName = e.ExerciseName,
                        BodyPartId = e.BodyPartId,
                        BodyPartName = e.BodyPartName,
                        Sets = e.Sets,
                        Repetitions = e.Repetitions,
                        StartWeight = e.StartWeight,
                        TargetWeight = e.TargetWeight,
                        IsKeyExercise = e.IsKeyExercise,
                        Order = e.Order
                    }).ToList()
            }).ToList(),
            TotalCount = result.TotalCount
        };
    }
}
