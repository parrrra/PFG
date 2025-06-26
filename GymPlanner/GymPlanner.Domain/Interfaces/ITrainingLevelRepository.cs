using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces
{
    public interface ITrainingLevelRepository
    {
        Task<List<TrainingLevel>> GetAllAsync();
    }
}