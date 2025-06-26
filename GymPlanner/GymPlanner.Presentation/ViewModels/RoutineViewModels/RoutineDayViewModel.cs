using GymPlanner.Presentation.ViewModels.TrainingTemplateViewModels;

namespace GymPlanner.Presentation.ViewModels.RoutineViewModels;

public class RoutineDayViewModel
{
    public Guid Id { get; set; }
    public int DayOfWeek { get; set; }
    public List<TrainingTemplateViewModel> Trainings { get; set; } = [];

    public Guid? NewTemplateId { get; set; }
}