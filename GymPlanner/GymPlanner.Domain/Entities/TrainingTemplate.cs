namespace GymPlanner.Domain.Entities;

public class TrainingTemplate
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Guid? UserId { get; set; }
    public List<TrainingTemplateExercise> Exercises { get; set; } = [];
}