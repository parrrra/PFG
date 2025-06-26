using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class TrainingExerciseEntity
{
    [Key]
    public Guid Id { get; set; }

    public Guid TrainingId { get; set; }

    public Guid ExerciseId { get; set; }

    [Range(1, 100)]
    public int Sets { get; set; }

    [Range(1, 100)]
    public int Repetitions { get; set; }

    public double StartWeight { get; set; }
    public double? TargetWeight { get; set; }

    public bool IsKeyExercise { get; set; }

    public int Order { get; set; }

    public string? Notes { get; set; }

    [ForeignKey(nameof(TrainingId))]
    public TrainingEntity Training { get; set; } = null!;

    [ForeignKey(nameof(ExerciseId))]
    public ExerciseEntity Exercise { get; set; } = null!;

    public ICollection<TrainingExerciseSetEntity> SetsDetails { get; set; } = [];
}
