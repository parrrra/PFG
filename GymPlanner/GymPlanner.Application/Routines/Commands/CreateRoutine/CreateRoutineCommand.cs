namespace GymPlanner.Application.Routines.Commands.CreateRoutine
{
    public class CreateRoutineCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public List<CreateRoutineDayModel> Days { get; set; } = [];
    }

    public class CreateRoutineDayModel
    {
        public int DayOfWeek { get; set; }
        public List<Guid> TrainingTemplateIds { get; set; } = [];
    }
}