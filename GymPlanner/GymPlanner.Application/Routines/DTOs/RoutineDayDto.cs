using GymPlanner.Application.TrainingTemplates.DTOs;

namespace GymPlanner.Application.Routines.DTOs
{
    public class RoutineDayDto
    {
        public Guid Id { get; set; }
        public int DayOfWeek { get; set; }
        public List<TrainingTemplateDto> TrainingTemplates { get; set; } = [];
    }
}