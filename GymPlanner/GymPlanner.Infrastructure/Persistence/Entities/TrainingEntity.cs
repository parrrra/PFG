using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class TrainingEntity
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; } = default!;

    public Guid TrainingTemplateId { get; set; }

    public DateOnly Date { get; set; }

    public bool IsCompleted { get; set; }

    [ForeignKey(nameof(Routine))]
    public Guid RoutineId { get; set; }


    [ForeignKey(nameof(UserId))]
    public ApplicationUserEntity User { get; set; } = null!;

    [ForeignKey(nameof(TrainingTemplateId))]
    public TrainingTemplateEntity TrainingTemplate { get; set; } = null!;

    public RoutineEntity Routine { get; set; } = null!;

    public ICollection<TrainingExerciseEntity> Exercises { get; set; } = [];
}
