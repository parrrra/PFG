using Microsoft.Extensions.DependencyInjection;
using GymPlanner.Common.CQRS;
using GymPlanner.Application.BodyParts.Commands.CreateBodyPart;
using GymPlanner.Application.BodyParts.Commands.UpdateBodyPart;
using GymPlanner.Application.BodyParts.Commands.DeleteBodyPart;
using GymPlanner.Application.BodyParts.Queries.GetBodyParts;
using GymPlanner.Common.Utils;
using GymPlanner.Application.BodyParts.DTOs;
using GymPlanner.Application.Users.Commands.RegisterUser;
using GymPlanner.Application.Users.Commands.LoginUser;
using GymPlanner.Application.Users.Commands.LogoutUser;
using GymPlanner.Application.TrainingLevel.Queries;
using GymPlanner.Application.TrainingLevel.DTOs;
using GymPlanner.Application.BodyParts.Queries.GetBodyPart;
using GymPlanner.Application.Exercises.Commands.CreateExercise;
using GymPlanner.Application.Exercises.Commands.UpdateExercise;
using GymPlanner.Application.Exercises.Commands.DeleteExercise;
using GymPlanner.Application.Exercises.Queries.GetExercises;
using GymPlanner.Application.Exercises.Queries.GetExercise;
using GymPlanner.Application.Exercises.DTOs;
using GymPlanner.Application.TrainingTemplates.Commands.CreateTrainingTemplate;
using GymPlanner.Application.TrainingTemplates.Queries.GetTrainingTemplate;
using GymPlanner.Application.TrainingTemplates.DTOs;
using GymPlanner.Application.TrainingTemplates.Commands.UpdateTrainingTemplate;
using GymPlanner.Application.TrainingTemplates.Commands.DeleteTrainingTemplate;
using GymPlanner.Application.TrainingTemplates.Queries.GetTrainingTemplates;
using GymPlanner.Application.Routines.Commands.CreateRoutine;
using GymPlanner.Application.Routines.Commands.UpdateRoutine;
using GymPlanner.Application.Routines.Commands.DeleteRoutine;
using GymPlanner.Application.Routines.Queries.GetRoutines;
using GymPlanner.Application.Routines.Queries.GetRoutine;
using GymPlanner.Application.Routines.DTOs;
using GymPlanner.Application.Routines.Commands.ActivateRoutine;
using GymPlanner.Application.Trainings.DTOs;
using GymPlanner.Application.Trainings.Queries.GetTrainings;
using GymPlanner.Application.Trainings.Commands.UpdateTraining;
using GymPlanner.Application.Trainings.Queries.GetTraining;
using GymPlanner.Application.Routines.Commands.DeactivateRoutine;
using GymPlanner.Application.Trainings.Queries.GetExerciseProgression;
using GymPlanner.Application.Emails.Commands;
using GymPlanner.Application.Users.Commands.ConfirmEmail;
namespace GymPlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Body Parts
        _ = services.AddScoped<ICommandHandler<CreateBodyPartCommand, Guid>, CreateBodyPartHandler>();
        _ = services.AddScoped<ICommandHandler<UpdateBodyPartCommand, bool>, UpdateBodyPartHandler>();
        _ = services.AddScoped<ICommandHandler<DeleteBodyPartCommand, bool>, DeleteBodyPartHandler>();
        _ = services.AddScoped<IQueryHandler<GetBodyPartsQuery, PaginatedResult<BodyPartDto>>, GetBodyPartsHandler>();
        _ = services.AddScoped<IQueryHandler<GetBodyPartQuery, BodyPartDto?>, GetBodyPartHandler>();

        //Users
        _ = services.AddScoped<ICommandHandler<RegisterUserCommand, bool>, RegisterUserHandler>();
        _ = services.AddScoped<ICommandHandler<ConfirmEmailCommand, bool>, ConfirmEmailHandler>();
        _ = services.AddScoped<ICommandHandler<LoginUserCommand, bool>, LoginUserHandler>();
        _ = services.AddScoped<ICommandHandler<LogoutUserCommand, bool>, LogoutUserHandler>();

        //Training Levels
        _ = services.AddScoped<IQueryHandler<GetTrainingLevelsQuery, List<TrainingLevelDto>>, GetTrainingLevelsHandler>();

        //Exercises
        _ = services.AddScoped<ICommandHandler<CreateExerciseCommand, Guid>, CreateExerciseHandler>();
        _ = services.AddScoped<ICommandHandler<UpdateExerciseCommand, bool>, UpdateExerciseHandler>();
        _ = services.AddScoped<ICommandHandler<DeleteExerciseCommand, bool>, DeleteExerciseHandler>();
        _ = services.AddScoped<IQueryHandler<GetExercisesQuery, PaginatedResult<ExerciseDto>>, GetExercisesHandler>();
        _ = services.AddScoped<IQueryHandler<GetExerciseQuery, ExerciseDto?>, GetExerciseHandler>();

        //TrainingTemplates
        _ = services.AddScoped<ICommandHandler<CreateTrainingTemplateCommand, Guid>, CreateTrainingTemplateHandler>();
        _ = services.AddScoped<ICommandHandler<UpdateTrainingTemplateCommand, bool>, UpdateTrainingTemplateHandler>();
        _ = services.AddScoped<ICommandHandler<DeleteTrainingTemplateCommand, bool>, DeleteTrainingTemplateHandler>();
        _ = services.AddScoped<IQueryHandler<GetTrainingTemplatesQuery, PaginatedResult<TrainingTemplateDto>>, GetTrainingTemplatesHandler>();
        _ = services.AddScoped<IQueryHandler<GetTrainingTemplateQuery, TrainingTemplateDto?>, GetTrainingTemplateHandler>();

        //Routines
        _ = services.AddScoped<ICommandHandler<CreateRoutineCommand, Guid>, CreateRoutineHandler>();
        _ = services.AddScoped<ICommandHandler<UpdateRoutineCommand, bool>, UpdateRoutineHandler>();
        _ = services.AddScoped<ICommandHandler<DeleteRoutineCommand, bool>, DeleteRoutineHandler>();
        _ = services.AddScoped<ICommandHandler<ActivateRoutineCommand, bool>, ActivateRoutineHandler>();
        _ = services.AddScoped<ICommandHandler<DeactivateRoutineCommand, bool>, DeactivateRoutineHandler>();
        _ = services.AddScoped<IQueryHandler<GetRoutinesQuery, PaginatedResult<RoutineDto>>, GetRoutinesHandler>();
        _ = services.AddScoped<IQueryHandler<GetRoutineQuery, RoutineDto?>, GetRoutineHandler>();

        //Training
        _ = services.AddScoped<ICommandHandler<UpdateTrainingCommand, bool>, UpdateTrainingHandler>();
        _ = services.AddScoped<IQueryHandler<GetTrainingQuery, TrainingDto?>, GetTrainingHandler>();
        _ = services.AddScoped<IQueryHandler<GetTrainingsQuery, List<TrainingDto>>, GetTrainingsHandler>();
        _ = services.AddScoped<IQueryHandler<GetExerciseProgressionQuery, List<TrainingExerciseProgressionDto>>, GetExerciseProgressionHandler>();

        //Emails
        _ = services.AddScoped<ICommandHandler<SendEmailCommand, bool>, SendEmailHandler>();



        return services;

    }
}
