using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Presentation.ViewModels.ExerciseViewModels
{
    public class ExerciseProgressionViewModel
    {
        public Guid TrainingLevelId { get; set; }
        public string TrainingLevelName { get; set; } = string.Empty;
        [Range(0, 1000)]
        public double WeeklyImprovement { get; set; }
    }
}