using GymPlanner.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Infrastructure;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUserEntity, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    public DbSet<BodyPartEntity> BodyParts => Set<BodyPartEntity>();

    public DbSet<TrainingLevelEntity> TrainingLevels => Set<TrainingLevelEntity>();

    public DbSet<ExerciseEntity> Exercises => Set<ExerciseEntity>();

    public DbSet<ExerciseTrainingLevelProgressionEntity> ExerciseTrainingLevelProgressions => Set<ExerciseTrainingLevelProgressionEntity>();

    public DbSet<TrainingTemplateEntity> TrainingTemplates => Set<TrainingTemplateEntity>();

    public DbSet<TrainingTemplateExerciseEntity> TrainingTemplateExercises => Set<TrainingTemplateExerciseEntity>();

    public DbSet<RoutineEntity> Routines => Set<RoutineEntity>();

    public DbSet<RoutineDayEntity> RoutineDays => Set<RoutineDayEntity>();

    public DbSet<RoutineDayTrainingTemplateEntity> RoutineDayTrainingTemplates => Set<RoutineDayTrainingTemplateEntity>();

    public DbSet<TrainingEntity> Trainings => Set<TrainingEntity>();
    public DbSet<TrainingExerciseEntity> TrainingExercises => Set<TrainingExerciseEntity>();

    public DbSet<TrainingExerciseSetEntity> TrainingExerciseSets => Set<TrainingExerciseSetEntity>();


}
