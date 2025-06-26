using GymPlanner.Application.TrainingLevel.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.TrainingLevel.Queries
{
    public class GetTrainingLevelsHandler : IQueryHandler<GetTrainingLevelsQuery, List<TrainingLevelDto>>
    {
        private readonly ITrainingLevelRepository _trainingLevelRepository;

        public GetTrainingLevelsHandler(ITrainingLevelRepository trainingLevelRepository)
        {
            _trainingLevelRepository = trainingLevelRepository;
        }

        public async Task<List<TrainingLevelDto>> Handle(GetTrainingLevelsQuery query, CancellationToken cancellation)
        {
            var trainingLevels = await _trainingLevelRepository.GetAllAsync();
            return trainingLevels.Select(tl => new TrainingLevelDto
            {
                Id = tl.Id,
                Name = tl.Name
            }).ToList();
        }
    }
}