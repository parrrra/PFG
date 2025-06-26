namespace GymPlanner.Application.Exercises.DTOs
{
    public class ExerciseProgressionDto
    {
        public Guid TrainingLevelId { get; set; }
        public string TrainingLevelName { get; set; } = string.Empty;
        public double WeeklyImprovement { get; set; }
    }
}