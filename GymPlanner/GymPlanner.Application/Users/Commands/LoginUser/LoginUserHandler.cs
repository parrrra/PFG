using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Users.Commands.LoginUser
{
    public class LoginUserHandler : ICommandHandler<LoginUserCommand, bool>
    {
        private readonly IUserLoginService _loginService;

        public LoginUserHandler(IUserLoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<bool> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            return await _loginService.LoginUserAsync(command.UserOrEmail, command.Password);
        }
    }
}
