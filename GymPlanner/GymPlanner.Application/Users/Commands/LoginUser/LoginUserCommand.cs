using GymPlanner.Application.Users.DTOs;

namespace GymPlanner.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand
    {
        public string UserOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}