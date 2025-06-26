using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class ExerciseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;

    [ForeignKey(nameof(BodyPart))]
    public Guid BodyPartId { get; set; }
    public BodyPartEntity BodyPart { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public List<ExerciseTrainingLevelProgressionEntity> TrainingLevelProgressions { get; set; } = new();
}
