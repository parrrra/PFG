using GymPlanner.Domain.Interfaces;
using GymPlanner.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymPlanner.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {

        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<ITrainingGenerationService, TrainingGenerationService>();


        return services;

    }
}
