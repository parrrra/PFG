using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces;

public interface ITrainingTemplateRepository
{
    Task<List<TrainingTemplate>> GetAllAsync(string? filter = null, Guid? userId = null);
    Task<TrainingTemplate?> GetByIdAsync(Guid id);
    Task CreateAsync(TrainingTemplate template);
    Task UpdateAsync(TrainingTemplate template);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync(string? filter = null, Guid? userId = null);
    Task<PaginatedResult<TrainingTemplate>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? userId = null);

}