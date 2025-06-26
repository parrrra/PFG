using GymPlanner.Application.Routines.DTOs;
using GymPlanner.Presentation.ViewModels.TrainingTemplateViewModels;
namespace GymPlanner.Presentation.ViewModels.RoutineViewModels
{
    public class RoutineMappings
    {
        public static RoutineViewModel ToViewModel(RoutineDto dto)
        {
            return new RoutineViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                UserId = dto.UserId,
                IsActive = dto.IsActive,
                Days = dto.Days.Select(ToViewModel).ToList()
            };
        }

        public static RoutineDayViewModel ToViewModel(RoutineDayDto dto)
        {
            return new RoutineDayViewModel
            {
                Id = dto.Id,
                DayOfWeek = dto.DayOfWeek,
                Trainings = dto.TrainingTemplates.Select(TrainingTemplateMappings.ToViewModel).ToList()
            };
        }

        public static RoutineDto ToDto(RoutineViewModel vm)
        {
            return new RoutineDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                UserId = vm.UserId,
                IsActive = vm.IsActive,
                Days = vm.Days.Select(ToDto).ToList()
            };
        }

        public static RoutineDayDto ToDto(RoutineDayViewModel vm)
        {
            return new RoutineDayDto
            {
                Id = vm.Id,
                DayOfWeek = vm.DayOfWeek,
                TrainingTemplates = vm.Trainings.Select(TrainingTemplateMappings.ToDto).ToList()
            };
        }

    }
}