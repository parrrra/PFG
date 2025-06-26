namespace GymPlanner.Domain.Entities;

public class TrainingExerciseSet
{
    public Guid Id { get; set; }

    public Guid TrainingExerciseId { get; set; }

    public int Order { get; set; }

    public int Repetitions { get; set; }

    public double Weight { get; set; }

}
