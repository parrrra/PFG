using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Application.Trainings.Queries.GetExerciseProgression
{
    public class GetExerciseProgressionQuery
    {
        public Guid UserId { get; set; }
        public Guid? ExerciseId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}