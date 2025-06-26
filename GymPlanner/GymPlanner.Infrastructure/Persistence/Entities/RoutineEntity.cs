using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class RoutineEntity
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

    public bool IsActive { get; set; }
    public ICollection<RoutineDayEntity> Days { get; set; } = [];

    public ICollection<TrainingEntity> Trainings { get; set; } = [];
}