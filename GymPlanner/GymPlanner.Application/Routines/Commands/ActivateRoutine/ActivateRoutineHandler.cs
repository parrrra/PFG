using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Commands.ActivateRoutine
{
    public class ActivateRoutineHandler : ICommandHandler<ActivateRoutineCommand, bool>
    {
        private readonly IRoutineService _routineService;

        public ActivateRoutineHandler(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        public async Task<bool> Handle(ActivateRoutineCommand command, CancellationToken cancellation)
        {
            await _routineService.ActivateExclusiveRoutineAsync(command.Id, command.UserId);
            return true;
        }
    }

}