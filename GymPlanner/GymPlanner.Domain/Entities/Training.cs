namespace GymPlanner.Domain.Entities;

public class Training
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = default!;

    public Guid TrainingTemplateId { get; set; }

    public string TrainingTemplateName { get; set; } = default!;

    public Guid RoutineId { get; set; }

    public DateOnly Date { get; set; }
    public bool IsCompleted { get; set; }

    public List<TrainingExercise> Exercises { get; set; } = new();
}
