using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.Core.UseCases.Animals;
using AmigosConCola.WebApi.Config.Auth;
using AmigosConCola.WebApi.Controllers;
using AmigosConCola.WebApi.Data.Repository;

namespace AmigosConCola.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogin(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<AuthController>("", x =>
        {
            var section = configuration.GetSection(nameof(AuthConfig));
            var address = section.GetValue<string>("BaseUrl")!;
            x.BaseAddress = new Uri(address);
        });
        services.Configure<AuthConfig>(configuration.GetSection(nameof(AuthConfig)));
        return services;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddScoped<IImageStorageRepository, ImageStorageRepository>();
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddAnimals(this IServiceCollection services)
    {
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<CreateAnimalUseCase>();
        services.AddScoped<GetAllAnimalsUseCase>();
        services.AddScoped<GetAnimalByIdUseCase>();
        services.AddScoped<CountAllAnimalsUseCase>();
        services.AddScoped<DeleteAnimalUseCase>();
        services.AddScoped<UpdateAnimalUseCase>();
        services.AddScoped<UpdateAnimalImageUrlUseCase>();
        services.AddScoped<StoreAnimalImageUseCase>();
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

    public static IServiceCollection AddInventory(this IServiceCollection services)
    {
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<CreateInventoryItemUseCase>();
        services.AddScoped<GetPaginatedInventoryItems>();
        services.AddScoped<CountAllInventoryItemsUseCase>();
        return services;
    }
}