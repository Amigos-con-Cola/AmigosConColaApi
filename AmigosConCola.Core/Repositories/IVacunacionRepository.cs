using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IVacunacionRepository
{
    /// <summary>
    ///     Create a new Vacunacion.
    /// </summary>
    /// <param name="parameters">Parameters needed to create the new vacunacion.</param>
    /// <returns>A vacunacion if successful.</returns>
    public Task<ErrorOr<Vacunacion>> Create(CreateVacunacionParams parameters);

    /// <summary>
    ///     Find all vacunaciones for the given animal.
    /// </summary>
    /// <param name="animalId">The id of the animal for which to find vacunaciones.</param>
    /// <returns>A list of all vacunaciones for the given animal</returns>
    public Task<ErrorOr<IEnumerable<Vacunacion>>> FindAll(int animalId);
}

public class CreateVacunacionParams
{
    public int IdAnimal { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly Date { get; set; }
    public string? ExamenPrevio { get; set; }
}