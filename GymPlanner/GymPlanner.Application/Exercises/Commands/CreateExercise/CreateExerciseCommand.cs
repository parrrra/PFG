using GymPlanner.Application.Exercises.DTOs;

namespace GymPlanner.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseCommand
{
    public string Name { get; set; } = string.Empty;
    public Guid BodyPartId { get; set; }
    public string? Description { get; set; }
    public List<ExerciseProgressionDto> TrainingLevelProgressions { get; set; } = [];
}