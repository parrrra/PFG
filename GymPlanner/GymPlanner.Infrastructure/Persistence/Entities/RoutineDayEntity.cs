using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class RoutineDayEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Routine))]
    public Guid RoutineId { get; set; }

    public RoutineEntity Routine { get; set; } = default!;

    public int DayOfWeek { get; set; }

    public List<RoutineDayTrainingTemplateEntity> Trainings { get; set; } = [];
}