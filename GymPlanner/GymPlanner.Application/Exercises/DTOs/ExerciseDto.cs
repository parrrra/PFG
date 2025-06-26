namespace GymPlanner.Application.Exercises.DTOs;

public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid BodyPartId { get; set; }
    public string? Description { get; set; }
    public List<ExerciseProgressionDto> TrainingLevelProgressions { get; set; } = [];
}