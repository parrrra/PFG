using GymPlanner.Application.Trainings.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Trainings.Queries.GetTrainings;

public class GetTrainingsHandler : IQueryHandler<GetTrainingsQuery, List<TrainingDto>>
{

    private readonly ITrainingRepository _trainingRepository;

    public GetTrainingsHandler(ITrainingRepository trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task<List<TrainingDto>> Handle(GetTrainingsQuery query, CancellationToken cancellation)
    {
        var trainings = await _trainingRepository.GetByUserAndMonthAsync(query.UserId, query.Year, query.Month);
        var trainingDtos = trainings.Select(t => new TrainingDto
        {
            Id = t.Id,
            UserId = t.UserId,
            TrainingTemplateId = t.TrainingTemplateId,
            TrainingTemplateName = t.TrainingTemplateName,
            RoutineId = t.RoutineId,
            Date = t.Date,
            Exercises = t.Exercises.Select(e => new TrainingExerciseDto
            {
                ExerciseName = e.ExerciseName,
                StartWeight = e.StartWeight,
                Order = e.Order
            }).ToList()
        }).ToList();
        return trainingDtos;
    }
}