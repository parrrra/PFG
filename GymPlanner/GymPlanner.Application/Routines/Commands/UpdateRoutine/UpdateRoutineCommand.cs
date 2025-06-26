namespace GymPlanner.Application.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public List<UpdateRoutineDayModel> Days { get; set; } = [];
}

public class UpdateRoutineDayModel
{
    public Guid? Id { get; set; }
    public int DayOfWeek { get; set; }
    public List<Guid> TrainingTemplateIds { get; set; } = [];
}
