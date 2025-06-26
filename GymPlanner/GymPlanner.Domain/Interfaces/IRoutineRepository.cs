using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Common.Utils;
using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces
{
    public interface IRoutineRepository
    {
        Task<List<Routine>> GetAllAsync(string? filter = null, Guid? userId = null);
        Task<Routine?> GetByIdAsync(Guid id);
        Task CreateAsync(Routine routine);
        Task UpdateAsync(Routine routine);
        Task DeleteAsync(Guid id);
        Task<int> CountAsync(string? filter = null, Guid? userId = null);
        Task<PaginatedResult<Routine>> GetPaginatedAsync(int pageNumber, int pageSize, string? filter = null, Guid? userId = null);
    }
}