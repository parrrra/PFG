using GymPlanner.Common.Constants;

namespace GymPlanner.Domain.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> RegisterUserAsync(string userName, string email, string password, Guid trainingLevelId, string role, string baseUrl);

        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}