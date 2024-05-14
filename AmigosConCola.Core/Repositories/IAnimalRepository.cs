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
    public Task<ErrorOr<IEnumerable<Animal>>> GetAll(GetAllAnimalsParams parameters);
}