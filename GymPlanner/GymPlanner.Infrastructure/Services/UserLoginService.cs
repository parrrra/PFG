using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymPlanner.Infrastructure.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly SignInManager<ApplicationUserEntity> _signInManager;

        public UserLoginService(SignInManager<ApplicationUserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            var res = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return res.Succeeded;
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}