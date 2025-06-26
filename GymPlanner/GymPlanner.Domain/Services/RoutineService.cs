using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Domain.Services;

public class RoutineService : IRoutineService
{
    private readonly IRoutineDataService _routineDataService;

    private readonly ITrainingGenerationService _trainingGenerationService;

    private readonly ITrainingRepository _trainingRepository;

    public RoutineService(IRoutineDataService routineDataService, ITrainingGenerationService trainingGenerationService, ITrainingRepository trainingRepository)
    {
        _routineDataService = routineDataService;
        _trainingGenerationService = trainingGenerationService;
        _trainingRepository = trainingRepository;
    }

    public async Task ActivateExclusiveRoutineAsync(Guid routineId, Guid userId)
    {
        await _routineDataService.ActivateAsync(routineId);
        await _trainingGenerationService.GenerateTrainingsAsync(routineId, userId);
    }

    public async Task DeactivateRoutineAsync(Guid routineId)
    {
        await _routineDataService.DeactivateAsync(routineId);
        await _trainingRepository.DeleteFromDateAsync(routineId, DateOnly.FromDateTime(DateTime.Now));
    }

}