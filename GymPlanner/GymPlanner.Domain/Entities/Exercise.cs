namespace GymPlanner.Domain.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid BodyPartId { get; set; }
    public string? Description { get; set; }
    public List<ExerciseTrainingLevelProgression> TrainingLevelProgressions { get; set; } = [];
}