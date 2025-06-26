using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.BodyParts.Commands.DeleteBodyPart
{
    public class DeleteBodyPartHandler : ICommandHandler<DeleteBodyPartCommand, bool>
    {
        private readonly IBodyPartRepository _repository;

        public DeleteBodyPartHandler(IBodyPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBodyPartCommand command, CancellationToken cancellation)
        {
            await _repository.DeleteAsync(command.Id);
            return true;
        }
    }
}
