namespace GymPlanner.Presentation.ViewModels.TrainingViewModels;

public class TrainingViewModel
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = default!;
    public Guid TrainingTemplateId { get; set; }
    public string TrainingTemplateName { get; set; } = string.Empty;
    public Guid RoutineId { get; set; }
    public DateOnly Date { get; set; }
    public bool IsCompleted { get; set; }
    public List<TrainingExerciseViewModel> Exercises { get; set; } = [];
}
