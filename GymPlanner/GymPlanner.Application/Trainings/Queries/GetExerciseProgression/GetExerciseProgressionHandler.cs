using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Application.Exercises.DTOs;
using GymPlanner.Application.Trainings.DTOs;
using GymPlanner.Common.CQRS;
using GymPlanner.Domain.Interfaces;

namespace GymPlanner.Application.Trainings.Queries.GetExerciseProgression
{
    public class GetExerciseProgressionHandler : IQueryHandler<GetExerciseProgressionQuery, List<TrainingExerciseProgressionDto>>
    {
        private readonly ITrainingRepository _repository;

        public GetExerciseProgressionHandler(ITrainingRepository repo)
        {
            _repository = repo;
        }

        public async Task<List<TrainingExerciseProgressionDto>> Handle(GetExerciseProgressionQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _repository.GetByUserAndDateRangeAsync(request.UserId, request.StartDate, request.EndDate, request.ExerciseId);

            return trainings
                .SelectMany(t => t.Exercises.Select(e => new TrainingExerciseProgressionDto
                {
                    Date = t.Date,
                    Weight = e.StartWeight,
                    ExerciseName = e.ExerciseName
                }))
                .OrderBy(e => e.Date)
                .ToList();
        }



    }

}