namespace GymPlanner.Application.TrainingTemplates.DTOs;

public class TrainingTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? UserId { get; set; }
    public List<TrainingTemplateExerciseDto> Exercises { get; set; } = [];
}