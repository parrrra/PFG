namespace GymPlanner.Application.Trainings.DTOs;

public class TrainingExerciseSetDto
{
    public Guid Id { get; set; }
    public Guid TrainingExerciseId { get; set; }
    public int Order { get; set; }
    public int Repetitions { get; set; }
    public double Weight { get; set; }
}