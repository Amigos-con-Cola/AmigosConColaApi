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
    /// <returns>A list of all animals.</returns>
    public Task<ErrorOr<IEnumerable<Animal>>> GetAll(PaginationParams parameters, GetAllAnimalsFilters filters);

    /// <summary>
    ///     Get a count of all animals.
    /// </summary>
    /// <returns>The total number of animals.</returns>
    public Task<int> CountAll();

    /// <summary>
    ///     Get an animal by its id.
    /// </summary>
    /// <param name="id">The id of the animal</param>
    /// <returns>An animal or an error.</returns>
    public Task<ErrorOr<Animal>> GetById(int id);
}