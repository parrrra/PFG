namespace GymPlanner.Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }

    public Guid TrainingLevelId { get; set; }
}