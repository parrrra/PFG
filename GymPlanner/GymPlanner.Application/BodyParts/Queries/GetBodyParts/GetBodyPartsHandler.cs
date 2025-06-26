using GymPlanner.Application.BodyParts.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Common.Utils;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.BodyParts.Queries.GetBodyParts
{
    public class GetBodyPartsHandler : IQueryHandler<GetBodyPartsQuery, PaginatedResult<BodyPartDto>>
    {
        private readonly IBodyPartRepository _repository;

        public GetBodyPartsHandler(IBodyPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<BodyPartDto>> Handle(GetBodyPartsQuery query, CancellationToken cancellation)
        {
            var paged = await _repository.GetPaginatedAsync(query.PageNumber, query.PageSize, query.Filter);
            return new PaginatedResult<BodyPartDto>
            {
                TotalCount = paged.TotalCount,
                Items = paged.Items.Select(x => new BodyPartDto { Id = x.Id, Name = x.Name }).ToList()
            };
        }
    }
}
