namespace GymPlanner.Application.Routines.DTOs
{
    public class RoutineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public List<RoutineDayDto> Days { get; set; } = [];
    }
}