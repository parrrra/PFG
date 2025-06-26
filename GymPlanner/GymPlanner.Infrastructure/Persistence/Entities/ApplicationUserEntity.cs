using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GymPlanner.Infrastructure.Persistence.Entities;

public class ApplicationUserEntity : IdentityUser
{
    public string? FullName { get; set; }

    [ForeignKey(nameof(TrainingLevel))]
    public Guid TrainingLevelId { get; set; }
    public TrainingLevelEntity TrainingLevel { get; set; } = default!;

    public List<TrainingTemplateEntity> TrainingTemplates { get; set; } = [];

    public List<TrainingEntity> Trainings { get; set; } = [];
    public List<RoutineEntity> Routines { get; set; } = [];
}
