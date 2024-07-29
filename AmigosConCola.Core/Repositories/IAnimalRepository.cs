using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IAnimalRepository
{
    /// <summary>
    ///     Create a new animal.
    /// </summary>
    /// <returns>An error or the created animal.</returns>
    public Task<ErrorOr<Animal>> Create(CreateAnimalParams parameters);

    /// <summary>
    ///     Get all animals.
    /// </summary>
    /// <param name="parameters">Parameters related to pagination.</param>
    /// <param name="filters">Filters to apply to the search.</param>
    /// <returns>A list of all animals.</returns>
    public Task<ErrorOr<IEnumerable<Animal>>> GetAll(PaginationParams parameters, GetAllAnimalsFilters filters);

    /// <summary>
    ///     Get a count of all animals.
    /// </summary>
    /// <param name="filters">Filters to apply to the search.</param>
    /// <returns>The total number of animals.</returns>
    public Task<int> CountAll(GetAllAnimalsFilters filters);

    /// <summary>
    ///     Get an animal by its id.
    /// </summary>
    /// <param name="id">The id of the animal</param>
    /// <returns>An animal or an error.</returns>
    public Task<ErrorOr<Animal>> GetById(int id);

    /// <summary>
    ///     Delete an animal by its id.
    /// </summary>
    /// <param name="id">The id of the animal</param>
    /// <returns>A bool or an error.</returns>
    public Task<ErrorOr<bool>> Delete(int id);
}

public class GetAllAnimalsFilters
{
    public AnimalSpecies? Species { get; set; }
    public string? Name { get; set; }
}

public sealed class CreateAnimalParams
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public AnimalGender Gender { get; set; }
    public string? ImageUrl { get; set; }
    public AnimalSpecies Species { get; set; }
    public string? Story { get; set; }
    public string Location { get; set; } = null!;
    public decimal Weight { get; set; }
    public string? Code { get; set; }
}