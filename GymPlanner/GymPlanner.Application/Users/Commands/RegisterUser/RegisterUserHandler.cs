using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, bool>
    {
        private readonly IUserRegistrationService _registrationService;

        public RegisterUserHandler(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<bool> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return await _registrationService.RegisterUserAsync(command.UserName, command.Email, command.Password, command.TrainingLevelId, command.Role, command.BaseUrl);
        }
    }
}
