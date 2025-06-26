using GymPlanner.Application.TrainingTemplates.DTOs;

namespace GymPlanner.Presentation.ViewModels.TrainingTemplateViewModels;

public static class TrainingTemplateMappings
{
    public static TrainingTemplateViewModel ToViewModel(TrainingTemplateDto dto)
    {
        return new TrainingTemplateViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Exercises = dto.Exercises
                .Select(e => new TrainingTemplateExerciseViewModel
                {
                    Id = e.Id,
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.ExerciseName,
                    BodyPartId = e.BodyPartId,
                    BodyPartName = e.BodyPartName,
                    Sets = e.Sets,
                    Repetitions = e.Repetitions,
                    StartWeight = e.StartWeight,
                    TargetWeight = e.TargetWeight,
                    IsKeyExercise = e.IsKeyExercise,
                    Order = e.Order
                })
                .ToList()
        };
    }

    public static TrainingTemplateDto ToDto(TrainingTemplateViewModel viewModel)
    {
        return new TrainingTemplateDto
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description,
            Exercises = viewModel.Exercises
                .Select(e => new TrainingTemplateExerciseDto
                {
                    Id = e.Id,
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.ExerciseName,
                    BodyPartId = e.BodyPartId,
                    BodyPartName = e.BodyPartName,
                    Sets = e.Sets,
                    Repetitions = e.Repetitions,
                    StartWeight = e.StartWeight,
                    TargetWeight = e.TargetWeight,
                    IsKeyExercise = e.IsKeyExercise,
                    Order = e.Order
                })
                .ToList()
        };
    }
}
