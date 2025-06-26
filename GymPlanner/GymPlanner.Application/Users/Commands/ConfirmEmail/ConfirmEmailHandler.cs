using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailHandler : ICommandHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRegistrationService _registrationService;

        public ConfirmEmailHandler(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<bool> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            return await _registrationService.ConfirmEmailAsync(command.UserId, command.Token);
        }
    }


}