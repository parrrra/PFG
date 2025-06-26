using GymPlanner.Application.TrainingTemplates.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingTemplates.Queries.GetTrainingTemplate;

public class GetTrainingTemplateHandler : IQueryHandler<GetTrainingTemplateQuery, TrainingTemplateDto?>
{
    private readonly ITrainingTemplateRepository _repository;

    public GetTrainingTemplateHandler(ITrainingTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrainingTemplateDto?> Handle(GetTrainingTemplateQuery query, CancellationToken cancellationToken)
    {
        var template = await _repository.GetByIdAsync(query.Id);
        if (template is null) return null;

        return new TrainingTemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            UserId = template.UserId,
            Exercises = template.Exercises.Select(e => new TrainingTemplateExerciseDto
            {
                Id = e.Id,
                ExerciseId = e.ExerciseId,
                Sets = e.Sets,
                Repetitions = e.Repetitions,
                StartWeight = e.StartWeight,
                TargetWeight = e.TargetWeight,
                IsKeyExercise = e.IsKeyExercise,
                Order = e.Order
            }).ToList()
        };
    }
}
