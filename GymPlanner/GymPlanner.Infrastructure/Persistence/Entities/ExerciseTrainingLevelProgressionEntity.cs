using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class ExerciseTrainingLevelProgressionEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Exercise))]
    public Guid ExerciseId { get; set; }
    public ExerciseEntity Exercise { get; set; } = default!;

    [ForeignKey(nameof(TrainingLevel))]
    public Guid TrainingLevelId { get; set; }
    public TrainingLevelEntity TrainingLevel { get; set; } = default!;

    public double WeeklyImprovement { get; set; }
}