namespace GymPlanner.Application.Users.DTOs
{
    public class LoginUserDto
    {
        public string UserOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}