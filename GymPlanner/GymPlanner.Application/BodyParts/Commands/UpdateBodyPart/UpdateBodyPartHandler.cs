using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Entities;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.BodyParts.Commands.UpdateBodyPart
{
    public class UpdateBodyPartHandler : ICommandHandler<UpdateBodyPartCommand, bool>
    {
        private readonly IBodyPartRepository _repository;

        public UpdateBodyPartHandler(IBodyPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateBodyPartCommand command, CancellationToken cancellation)
        {
            var bodyPart = new BodyPart(command.Id, command.Name);
            await _repository.UpdateAsync(bodyPart);
            return true;
        }
    }
}
