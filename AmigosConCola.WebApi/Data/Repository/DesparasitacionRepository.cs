using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using ErrorOr;

namespace AmigosConCola.WebApi.Data.Repository;

public sealed class DesparasitacionRepository : IDesparasitacionRepository
{
    private readonly ApplicationDbContext _db;

    public DesparasitacionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ErrorOr<Desparasitacion>> Create(CreateDesparasitacionParams parameters)
    {
        var dto = new DesparasitacionDto
        {
            IdAnimal = parameters.IdAnimal,
            Tipo = parameters.Tipo,
            Fecha = parameters.Fecha,
            Producto = parameters.Producto,
            Peso = parameters.Peso,
            Formato = parameters.Formato,
        };

        var result = await _db.AddAsync(dto);
        await _db.SaveChangesAsync();

        if (result is null || result.Entity.Id is null)
        {
            return Error.Unexpected(description: "Failed to create the desparasitacion");
        }

        return new Desparasitacion
        {
            Id = (int)result.Entity.Id,
            IdAnimal = result.Entity.IdAnimal,
            Tipo = result.Entity.Tipo,
            Fecha = result.Entity.Fecha,
            Producto = result.Entity.Producto,
            Peso = result.Entity.Peso,
            Formato = result.Entity.Formato,
        };
    }

    public Task<IEnumerable<Desparasitacion>> FindAll(int animalId)
    {
        return Task.FromResult(_db.Desparasitaciones
            .Where(x => x.IdAnimal == animalId)
            .Select(x => new Desparasitacion
            {
                Id = (int)x.Id!,
                IdAnimal = x.IdAnimal,
                Tipo = x.Tipo,
                Fecha = x.Fecha,
                Producto = x.Producto,
                Peso = x.Peso,
                Formato = x.Formato,
            })
            .AsEnumerable());
    }
}
