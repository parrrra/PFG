using GymPlanner.Common.Constants;
using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymPlanner.Infrastructure.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly IEmailSender _emailSender;

        public UserRegistrationService(UserManager<ApplicationUserEntity> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<bool> RegisterUserAsync(string userName, string email, string password, Guid trainingLevelId, string role, string baseUrl)
        {
            var user = new ApplicationUserEntity
            {
                UserName = userName,
                Email = email,
                TrainingLevelId = trainingLevelId
            };

            var create = await _userManager.CreateAsync(user, password);
            if (!create.Succeeded) return false;

            var addRole = await _userManager.AddToRoleAsync(user, role);
            if (!addRole.Succeeded) return false;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var confirmationUrl = $"{baseUrl}confirmemail?userId={user.Id}&token={encodedToken}";

            await _emailSender.SendAsync(
                user.Email,
                "Confirma tu cuenta",
                $"Haz clic en este enlace para confirmar tu cuenta: <a href=\"{confirmationUrl}\">Confirmar cuenta</a>");

            return true;
        }


        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var decodedToken = Uri.UnescapeDataString(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            return result.Succeeded;
        }

    }
}