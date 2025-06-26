namespace GymPlanner.Domain.Entities;

public class Routine
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public List<RoutineDay> Days { get; set; } = [];

    public bool IsActive { get; set; }
}