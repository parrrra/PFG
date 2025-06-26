namespace GymPlanner.Domain.Interfaces;

public interface IUserLoginService
{
    Task<bool> LoginUserAsync(string email, string password);
    Task LogoutUserAsync();
}
