using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Data.Repository;

namespace AmigosConCola.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAnimals(this IServiceCollection services)
    {
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<CreateAnimalUseCase>();
        services.AddScoped<GetAllAnimalsUseCase>();
        services.AddScoped<GetAnimalByIdUseCase>();
        services.AddScoped<CountAllAnimalsUseCase>();
        services.AddScoped<DeleteAnimalUseCase>();
        return services;
    }

    public static IServiceCollection AddVacunaciones(this IServiceCollection services)
    {
        services.AddScoped<IVacunacionRepository, VacunacionRepository>();
        services.AddScoped<CreateVacunacionUseCase>();
        services.AddScoped<FindAllVacunacionesUseCase>();
        return services;
    }

    public static IServiceCollection AddDesparasitaciones(this IServiceCollection services)
    {
        services.AddScoped<IDesparasitacionRepository, DesparasitacionRepository>();
        services.AddScoped<CreateDesparasitacionUseCase>();
        services.AddScoped<FindAllDesparasitacionesUseCase>();
        return services;
    }

    public static IServiceCollection AddAseos(this IServiceCollection services)
    {
        services.AddScoped<IAseosRepository, AseoRepository>();
        services.AddScoped<CreateAseoUseCase>();
        services.AddScoped<FindAllAseosUseCase>();
        return services;
    }

    public static IServiceCollection AddPesos(this IServiceCollection services)
    {
        services.AddScoped<IPesosRepository, PesoRepository>();
        services.AddScoped<CreatePesoUseCase>();
        services.AddScoped<FindAllPesosUseCase>();
        return services;
    }
}