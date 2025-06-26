using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.BodyParts.Commands.CreateBodyPart
{
    public class CreateBodyPartHandler : ICommandHandler<CreateBodyPartCommand, Guid>
    {
        private readonly IBodyPartRepository _repository;

        public CreateBodyPartHandler(IBodyPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateBodyPartCommand command, CancellationToken cancellation)
        {
            var bodyPart = new BodyPart(command.Name);
            await _repository.CreateAsync(bodyPart);
            return bodyPart.Id;
        }
    }
}
