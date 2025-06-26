namespace GymPlanner.Domain.Entities;

public class TrainingExercise
{
    public Guid Id { get; set; }

    public Guid TrainingId { get; set; }

    public Guid ExerciseId { get; set; }

    public string ExerciseName { get; set; } = default!;

    public int Sets { get; set; }

    public int Repetitions { get; set; }

    public double StartWeight { get; set; }

    public double? TargetWeight { get; set; }

    public bool IsKeyExercise { get; set; }

    public int Order { get; set; }

    public string? Notes { get; set; }

    public List<TrainingExerciseSet> TrainingExerciseSets { get; set; } = [];
}
