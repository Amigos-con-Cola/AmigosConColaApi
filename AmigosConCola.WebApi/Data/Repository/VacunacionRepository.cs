using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace AmigosConCola.WebApi.Data.Repository;

public class VacunacionRepository : IVacunacionRepository
{
    private readonly ApplicationDbContext _db;

    public VacunacionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ErrorOr<Vacunacion>> Create(CreateVacunacionParams parameters)
    {
        var animalDto = await _db.Animals.Where(x => x.Id == parameters.IdAnimal).FirstOrDefaultAsync();

        if (animalDto is null)
        {
            return Error.NotFound(description: "There is not animal with that id");
        }

        var dto = new VacunacionDto
        {
            IdAnimal = parameters.IdAnimal,
            Name = parameters.Name,
            Date = parameters.Date
        };

        var result = await _db.Vacunaciones.AddAsync(dto);

        await _db.SaveChangesAsync();

        return result.Entity.ToDomain();
    }

    public async Task<ErrorOr<IEnumerable<Vacunacion>>> FindAll(int animalId)
    {
        var animal = await _db.Animals
            .Where(x => x.Id == animalId)
            .Include(x => x.Vacunaciones)
            .FirstOrDefaultAsync();


        if (animal is null)
        {
            return Error.NotFound(description: "No such animal with that id");
        }

        return animal.Vacunaciones
            .Select(x => x.ToDomain())
            .ToErrorOr();
    }
}