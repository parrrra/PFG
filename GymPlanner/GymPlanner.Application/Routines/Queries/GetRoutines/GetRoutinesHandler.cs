using GymPlanner.Application.Routines.DTOs;
using GymPlanner.Application.TrainingTemplates.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Common.Utils;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Routines.Queries.GetRoutines;

public class GetRoutinesHandler : IQueryHandler<GetRoutinesQuery, PaginatedResult<RoutineDto>>
{
    private readonly IRoutineRepository _repository;

    public GetRoutinesHandler(IRoutineRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<RoutineDto>> Handle(GetRoutinesQuery query, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPaginatedAsync(query.PageNumber, query.PageSize, query.Filter, query.UserId);

        return new PaginatedResult<RoutineDto>
        {
            Items = result.Items.Select(r => new RoutineDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                UserId = r.UserId,
                IsActive = r.IsActive,
                Days = r.Days.Select(d => new RoutineDayDto
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
            }).ToList(),
            TotalCount = result.TotalCount
        };
    }
}
