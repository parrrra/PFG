namespace GymPlanner.Domain.Services;

public interface ITrainingGenerationService
{
    Task GenerateTrainingsAsync(Guid routineId, Guid userId);
}
