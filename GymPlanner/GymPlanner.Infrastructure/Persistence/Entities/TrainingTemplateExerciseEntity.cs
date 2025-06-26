using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class TrainingTemplateExerciseEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(TrainingTemplate))]
    public Guid TrainingTemplateId { get; set; }
    public TrainingTemplateEntity TrainingTemplate { get; set; } = default!;

    [ForeignKey(nameof(Exercise))]
    public Guid ExerciseId { get; set; }
    public ExerciseEntity Exercise { get; set; } = default!;

    [Range(1, 100)]
    public int Sets { get; set; }

    [Range(1, 100)]
    public int Repetitions { get; set; }

    public double StartWeight { get; set; }

    public double? TargetWeight { get; set; }

    public bool IsKeyExercise { get; set; }

    public int Order { get; set; }
}
