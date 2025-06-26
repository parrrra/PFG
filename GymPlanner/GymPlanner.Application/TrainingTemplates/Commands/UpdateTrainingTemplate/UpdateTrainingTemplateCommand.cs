namespace GymPlanner.Application.TrainingTemplates.Commands.UpdateTrainingTemplate;

public class UpdateTrainingTemplateCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? UserId { get; set; }
    public List<UpdateTrainingTemplateExerciseModel> Exercises { get; set; } = [];
}

public class UpdateTrainingTemplateExerciseModel
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public double StartWeight { get; set; }
    public double? TargetWeight { get; set; }
    public bool IsKeyExercise { get; set; }
    public int Order { get; set; }
}
