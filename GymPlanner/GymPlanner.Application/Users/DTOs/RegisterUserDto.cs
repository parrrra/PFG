namespace GymPlanner.Application.Users.DTOs
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid TrainingLevelId { get; set; }
    }
}