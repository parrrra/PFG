using GymPlanner.Domain.Entities;

namespace GymPlanner.Domain.Interfaces;

public interface ITrainingRepository
{
    Task CreateAsync(Training training);

    Task<List<Training>> GetByUserAndMonthAsync(Guid userId, int year, int month);

    Task<Training?> GetByIdAsync(Guid id);

    Task UpdateAsync(Training training);

    Task DeleteFromDateAsync(Guid routineId, DateOnly fromDate);

    Task<List<Training>> GetByUserAndDateRangeAsync(Guid userId, DateOnly? startDate, DateOnly? endDate, Guid? exerciseId = null);


}