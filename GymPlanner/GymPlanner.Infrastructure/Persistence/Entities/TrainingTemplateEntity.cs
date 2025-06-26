using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class TrainingTemplateEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }
    public ApplicationUserEntity? User { get; set; }

    public ICollection<TrainingTemplateExerciseEntity> Exercises { get; set; } = [];

    public ICollection<TrainingEntity> Trainings { get; set; } = [];
}