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


    /// <summary>
    ///     Update an animal by its id.
    /// </summary>
    /// <param name="id">The id of the animal to update.</param>
    /// <param name="parameters">The data to update in the animal.</param>
    /// <returns>The updated animal or an error.</returns>
    public Task<ErrorOr<Animal>> Update(int id, UpdateAnimalParams parameters);

    /// <summary>
    ///     Update the animal's image url.
    /// </summary>
    /// <param name="id">The id of the animal for which to update the url</param>
    /// <param name="imageUrl">The new image url</param>
    /// <returns>The updated animal, or an error.</returns>
    public Task<ErrorOr<Animal>> UpdateImageUrl(int id, string imageUrl);
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

public sealed class UpdateAnimalParams
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? ImageUrl { get; set; }
    public string? Species { get; set; }
    public string? Story { get; set; }
    public string? Location { get; set; }
    public decimal? Weight { get; set; }
    public string? Code { get; set; }
    public bool? Adopted { get; set; }
}