using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces
{
    public interface IBodyPartRepository
    {
        Task<List<BodyPart>> GetAllAsync(string? filter = null);
        Task<BodyPart?> GetByIdAsync(Guid id);
        Task CreateAsync(BodyPart bodyPart);
        Task UpdateAsync(BodyPart bodyPart);
        Task DeleteAsync(Guid id);
        Task<int> CountAsync(string? filter = null);
        Task<PaginatedResult<BodyPart>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null);
    }
}
