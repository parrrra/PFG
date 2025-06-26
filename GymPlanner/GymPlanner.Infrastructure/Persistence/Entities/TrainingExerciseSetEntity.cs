using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class TrainingExerciseSetEntity
{
    [Key]
    public Guid Id { get; set; }

    public Guid TrainingExerciseId { get; set; }

    public int Order { get; set; }

    public int Repetitions { get; set; }

    public double Weight { get; set; }


    [ForeignKey(nameof(TrainingExerciseId))]
    public TrainingExerciseEntity TrainingExercise { get; set; } = null!;
}