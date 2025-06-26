using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Application.Trainings.DTOs
{
    public class TrainingExerciseProgressionDto
    {
        public DateOnly Date { get; set; }
        public double Weight { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
    }
}