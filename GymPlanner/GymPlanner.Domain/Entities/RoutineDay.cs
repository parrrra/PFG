namespace GymPlanner.Domain.Entities;

public class RoutineDay
{
    public Guid Id { get; set; }
    public int DayOfWeek { get; set; }
    public List<TrainingTemplate> TrainingTemplates { get; set; } = [];
}