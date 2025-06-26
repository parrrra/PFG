using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Commands.CreateRoutine
{
    public class CreateRoutineHandler : ICommandHandler<CreateRoutineCommand, Guid>
    {
        private readonly IRoutineRepository _routineRepo;

        public CreateRoutineHandler(IRoutineRepository routineRepo)
        {
            _routineRepo = routineRepo;
        }

        public async Task<Guid> Handle(CreateRoutineCommand command, CancellationToken cancellation)
        {
            var routine = new Routine
            {
                Name = command.Name,
                Description = command.Description,
                UserId = command.UserId,
                Days = command.Days.Select(day => new RoutineDay
                {
                    DayOfWeek = day.DayOfWeek,
                    TrainingTemplates = day.TrainingTemplateIds.Select(id =>
                        new TrainingTemplate
                        {
                            Id = id,
                        }).ToList()
                }).ToList()
            };

            await _routineRepo.CreateAsync(routine);
            return routine.Id;
        }
    }

}