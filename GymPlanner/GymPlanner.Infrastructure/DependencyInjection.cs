using GymPlanner.Domain.Interfaces;
using GymPlanner.Infrastructure.Persistence.Entities;
using GymPlanner.Infrastructure.Repositories;
using GymPlanner.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
            o.UseSqlServer(cfg.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUserEntity, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); ;

        services.AddScoped<IUserLoginService, UserLoginService>();

        services.AddScoped<IBodyPartRepository, BodyPartRepository>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<ITrainingLevelRepository, TrainingLevelRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<ITrainingTemplateRepository, TrainingTemplateRepository>();
        services.AddScoped<IRoutineRepository, RoutineRepository>();
        services.AddScoped<IRoutineDataService, RoutineDataService>();

        services.AddScoped<ITrainingRepository, TrainingRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IEmailSender, GmailEmailSender>();


        return services;
    }
}
