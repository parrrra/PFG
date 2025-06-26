using GymPlanner.Application.Users.DTOs;
using GymPlanner.Common.Constants;

namespace GymPlanner.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = Roles.User;
        public Guid TrainingLevelId { get; set; }
        public string BaseUrl { get; set; } = string.Empty;

    }
}