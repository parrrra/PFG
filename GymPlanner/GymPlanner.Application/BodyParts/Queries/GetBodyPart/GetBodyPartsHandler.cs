using GymPlanner.Application.BodyParts.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.BodyParts.Queries.GetBodyPart
{
    public class GetBodyPartHandler : IQueryHandler<GetBodyPartQuery, BodyPartDto?>
    {
        private readonly IBodyPartRepository _repository;

        public GetBodyPartHandler(IBodyPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<BodyPartDto?> Handle(GetBodyPartQuery query, CancellationToken cancellation)
        {
            var bodyPart = await _repository.GetByIdAsync(query.Id);
            return bodyPart is null ? null : new BodyPartDto { Id = bodyPart.Id, Name = bodyPart.Name };
        }
    }
}
