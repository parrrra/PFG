namespace GymPlanner.Application.Trainings.Commands.UpdateTraining;

public class UpdateTrainingCommand
{
    public Guid Id { get; set; }

    public bool IsCompleted { get; set; }
    public List<UpdateTrainingExerciseModel> Exercises { get; set; } = [];
}

public class UpdateTrainingExerciseModel
{
    public Guid Id { get; set; }
    public int Sets { get; set; }
    public int Repetitions { get; set; }
    public double StartWeight { get; set; }
    public double? TargetWeight { get; set; }
    public bool IsKeyExercise { get; set; }
    public string? Notes { get; set; }
    public int Order { get; set; }
    public List<UpdateTrainingExerciseSetModel> TrainingExerciseSets { get; set; } = [];
}

public class UpdateTrainingExerciseSetModel
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public int Repetitions { get; set; }
    public double Weight { get; set; }
}