namespace GymPlanner.Application.Trainings.DTOs;

public class TrainingExerciseDto
{
    public Guid Id { get; set; }
    public Guid TrainingId { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public double StartWeight { get; set; }
    public double? TargetWeight { get; set; }
    public bool IsKeyExercise { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
    public List<TrainingExerciseSetDto> TrainingExerciseSets { get; set; } = [];
}