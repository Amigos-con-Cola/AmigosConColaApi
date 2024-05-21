using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using ErrorOr;

namespace AmigosConCola.WebApi.Data.Repository;

public class AnimalRepository : IAnimalRepository
{
    private readonly ApplicationDbContext _db;

    public AnimalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ErrorOr<Animal>> Create(CreateAnimalParams parameters)
    {
        var dto = new AnimalDto
        {
            Name = parameters.Name,
            Age = parameters.Age,
            Gender = parameters.Gender.ToString(),
            ImageUrl = parameters.ImageUrl,
            Species = parameters.Species.ToString(),
            Weight = parameters.Weight,
            Story = parameters.Story,
            Code = parameters.Code, Location = parameters.Location
        };

        var result = await _db.Animals.AddAsync(dto);

        await _db.SaveChangesAsync();

        return result.Entity.ToDomain();
    }

    public async Task<ErrorOr<IEnumerable<Animal>>> GetAll(PaginationParams parameters)
    {
        return _db
            .Animals
            .OrderBy(x => x.Id)
            .Skip((parameters.Page - 1) * parameters.PerPage)
            .Take(parameters.PerPage)
            .Select(x => x.ToDomain().Value)
            .AsEnumerable()
            .ToErrorOr();
    }
}