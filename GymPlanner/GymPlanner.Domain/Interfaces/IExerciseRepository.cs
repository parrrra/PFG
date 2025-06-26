using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces;

public interface IExerciseRepository
{
    Task<List<Exercise>> GetAllAsync(string? filter = null);
    Task<Exercise?> GetByIdAsync(Guid id);
    Task CreateAsync(Exercise exercise);
    Task UpdateAsync(Exercise exercise);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync(string? filter = null);
    Task<PaginatedResult<Exercise>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? bodyPartId = null);
}