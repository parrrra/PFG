using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Application.Trainings.DTOs
{
    public class TrainingDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = default!;

        public Guid TrainingTemplateId { get; set; }

        public string TrainingTemplateName { get; set; } = default!;

        public Guid RoutineId { get; set; }

        public DateOnly Date { get; set; }

        public bool IsCompleted { get; set; }

        public List<TrainingExerciseDto> Exercises { get; set; } = [];
    }
}