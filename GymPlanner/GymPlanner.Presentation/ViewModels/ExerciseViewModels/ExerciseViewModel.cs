using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Presentation.ViewModels.ExerciseViewModels
{
    public class ExerciseViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid BodyPartId { get; set; }

        public string? Description { get; set; }

        public List<ExerciseProgressionViewModel> TrainingLevelProgressions { get; set; } = [];
    }
}