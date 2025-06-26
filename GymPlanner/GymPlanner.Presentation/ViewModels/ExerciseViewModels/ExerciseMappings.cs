using GymPlanner.Application.Exercises.DTOs;

namespace GymPlanner.Presentation.ViewModels.ExerciseViewModels;

public static class ExerciseMappings
{
    public static ExerciseViewModel ToViewModel(ExerciseDto dto)
    {
        return new ExerciseViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            BodyPartId = dto.BodyPartId,
            Description = dto.Description,
            TrainingLevelProgressions = dto.TrainingLevelProgressions.Select(x => new ExerciseProgressionViewModel
            {
                TrainingLevelId = x.TrainingLevelId,
                TrainingLevelName = x.TrainingLevelName,
                WeeklyImprovement = x.WeeklyImprovement
            }).ToList()
        };
    }

    public static ExerciseDto ToDto(ExerciseViewModel vm)
    {
        return new ExerciseDto
        {
            Id = vm.Id,
            Name = vm.Name,
            BodyPartId = vm.BodyPartId,
            Description = vm.Description,
            TrainingLevelProgressions = vm.TrainingLevelProgressions.Select(x => new ExerciseProgressionDto
            {
                TrainingLevelId = x.TrainingLevelId,
                TrainingLevelName = x.TrainingLevelName,
                WeeklyImprovement = x.WeeklyImprovement
            }).ToList()
        };
    }
}
