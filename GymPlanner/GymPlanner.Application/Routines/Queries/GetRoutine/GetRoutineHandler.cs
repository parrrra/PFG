using GymPlanner.Application.Routines.DTOs;
using GymPlanner.Application.TrainingTemplates.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GymPlanner.Application.Routines.Queries.GetRoutine;

public class GetRoutineHandler : IQueryHandler<GetRoutineQuery, RoutineDto?>
{
    private readonly IRoutineRepository _repository;

    public GetRoutineHandler(IRoutineRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoutineDto?> Handle(GetRoutineQuery query, CancellationToken cancellationToken)
    {
        var routine = await _repository.GetByIdAsync(query.Id);
        if (routine is null) return null;

        return new RoutineDto
        {
            Id = routine.Id,
            Name = routine.Name,
            Description = routine.Description,
            UserId = routine.UserId,
            IsActive = routine.IsActive,
            Days = routine.Days.Select(d => new RoutineDayDto
            {
                Id = d.Id,
                DayOfWeek = d.DayOfWeek,
                TrainingTemplates = d.TrainingTemplates.Select(t => new TrainingTemplateDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToList()
            }).ToList()
        };
    }
}
