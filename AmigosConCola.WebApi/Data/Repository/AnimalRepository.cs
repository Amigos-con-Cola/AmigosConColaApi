using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Entities;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace AmigosConCola.WebApi.Data.Repository;

public class AnimalRepository : IAnimalRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AnimalRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Animal>> Create(CreateAnimalParams parameters)
    {
        var dto = new AnimalEntity
        {
            Name = parameters.Name,
            Age = parameters.Age,
            Gender = parameters.Gender.ToString(),
            ImageUrl = parameters.ImageUrl,
            Species = parameters.Species.ToString(),
            Weight = parameters.Weight,
            Story = parameters.Story,
            Code = parameters.Code,
            Location = parameters.Location
        };

        var result = await _db.Animals.AddAsync(dto);

        await _db.SaveChangesAsync();

        return result.Entity.ToDomain();
    }

    public Task<ErrorOr<IEnumerable<Animal>>> GetAll(PaginationParams parameters, GetAllAnimalsFilters filters)
    {
        var query = _db.Animals.AsQueryable();

        if (filters.Species is not null)
            query = query.Where(x => x.Species.ToLower() == filters.Species.ToString()!.ToLower());

        if (filters.Name is not null) query = query.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));

        var result = query
            .OrderByDescending(x => x.Id)
            .Skip((parameters.Page - 1) * parameters.PerPage)
            .Take(parameters.PerPage)
            .Select(x => x.ToDomain().Value)
            .AsEnumerable()
            .ToErrorOr();

        return Task.FromResult(result);
    }

    public async Task<int> CountAll(GetAllAnimalsFilters filters)
    {
        var query = _db.Animals.AsQueryable();

        if (filters.Species is not null)
            query = query.Where(x => x.Species.ToLower() == filters.Species.ToString()!.ToLower());

        return await query.CountAsync();
    }

    public async Task<ErrorOr<Animal>> GetById(int id)
    {
        var animal = await _db.Animals.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (animal is null) return Error.NotFound(description: $"There is no animal with the id {id}");

        return animal.ToDomain();
    }

    public async Task<ErrorOr<bool>> Delete(int id)
    {
        var animal = await _db.Animals.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (animal is null) return Error.NotFound(description: $"There is no animal with the id {id}");

        _db.Animals.Remove(animal);

        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"An error occurred while deleting the animal: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Animal>> Update(int id, UpdateAnimalParams parameters)
    {
        var animal = await _db.Animals.FindAsync(id);

        if (animal is null)
            return Error.NotFound(description: "No animal with that id");

        animal.Name = parameters.Name ?? animal.Name;
        animal.Age = parameters.Age ?? animal.Age;
        animal.Gender = parameters.Gender ?? animal.Gender;
        animal.ImageUrl = parameters.ImageUrl ?? animal.ImageUrl;
        animal.Species = parameters.Species ?? animal.Species;
        animal.Story = parameters.Story ?? animal.Story;
        animal.Location = parameters.Location ?? animal.Location;
        animal.Weight = parameters.Weight ?? animal.Weight;
        animal.Code = parameters.Code ?? animal.Code;
        animal.Adopted = parameters.Adopted ?? animal.Adopted;

        try
        {
            await _db.SaveChangesAsync();
            return _mapper.Map<Animal>(animal);
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: $"Error while updating the animal: {ex}");
        }
    }

    public async Task<ErrorOr<Animal>> UpdateImageUrl(int id, string imageUrl)
    {
        var animalEntity = await _db.Animals.FindAsync(id);
        if (animalEntity is null)
            return Error.NotFound(description: "No animal with the given ID");

        animalEntity.ImageUrl = imageUrl;

        await _db.SaveChangesAsync();

        return _mapper.Map<Animal>(animalEntity);
    }
}