using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Users.Commands.LogoutUser
{
    public class LogoutUserHandler : ICommandHandler<LogoutUserCommand, bool>
    {
        private readonly IUserLoginService _loginService;

        public LogoutUserHandler(IUserLoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<bool> Handle(LogoutUserCommand command, CancellationToken cancellationToken)
        {
            await _loginService.LogoutUserAsync();
            return true;
        }
    }
}
