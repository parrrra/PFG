using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Commands.UpdateRoutine;

public class UpdateRoutineHandler : ICommandHandler<UpdateRoutineCommand, bool>
{
    private readonly IRoutineRepository _routineRepository;

    public UpdateRoutineHandler(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;
    }

    public async Task<bool> Handle(UpdateRoutineCommand command, CancellationToken cancellationToken)
    {
        var routine = new Routine
        {
            Id = command.Id,
            Name = command.Name,
            Description = command.Description,
            UserId = command.UserId,
            Days = command.Days.Select(day => new RoutineDay
            {
                Id = day.Id ?? Guid.NewGuid(),
                DayOfWeek = day.DayOfWeek,
                TrainingTemplates = day.TrainingTemplateIds
                    .Select(id => new TrainingTemplate
                    {
                        Id = id,
                        Name = string.Empty,
                        Description = null,
                        UserId = Guid.Empty,
                        Exercises = []
                    }).ToList()
            }).ToList()
        };

        await _routineRepository.UpdateAsync(routine);
        return true;
    }
}
