namespace GymPlanner.Application.TrainingTemplates.DTOs;

public class TrainingTemplateExerciseDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public Guid BodyPartId { get; set; }
    public string BodyPartName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public double StartWeight { get; set; }
    public double? TargetWeight { get; set; }
    public bool IsKeyExercise { get; set; }
    public int Order { get; set; }
}