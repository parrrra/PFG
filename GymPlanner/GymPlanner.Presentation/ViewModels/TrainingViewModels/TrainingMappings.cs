using GymPlanner.Application.Trainings.DTOs;

namespace GymPlanner.Presentation.ViewModels.TrainingViewModels;

public static class TrainingMappings
{
    public static TrainingViewModel ToViewModel(TrainingDto dto) => new()
    {
        Id = dto.Id,
        UserId = dto.UserId,
        TrainingTemplateId = dto.TrainingTemplateId,
        TrainingTemplateName = dto.TrainingTemplateName,
        RoutineId = dto.RoutineId,
        Date = dto.Date,
        IsCompleted = dto.IsCompleted,
        Exercises = dto.Exercises.Select(ToViewModel).ToList()
    };

    public static TrainingExerciseViewModel ToViewModel(TrainingExerciseDto dto) => new()
    {
        Id = dto.Id,
        TrainingId = dto.TrainingId,
        ExerciseId = dto.ExerciseId,
        ExerciseName = dto.ExerciseName,
        Sets = dto.Sets,
        Repetitions = dto.Repetitions,
        StartWeight = dto.StartWeight,
        TargetWeight = dto.TargetWeight,
        IsKeyExercise = dto.IsKeyExercise,
        Order = dto.Order,
        Notes = dto.Notes,
        TrainingExerciseSets = dto.TrainingExerciseSets.Select(ToViewModel).ToList()
    };

    public static TrainingExerciseSetViewModel ToViewModel(TrainingExerciseSetDto dto) => new()
    {
        Id = dto.Id,
        Order = dto.Order,
        Repetitions = dto.Repetitions,
        Weight = dto.Weight
    };
}
