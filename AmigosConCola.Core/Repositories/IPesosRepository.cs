using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IPesosRepository
{
    /// <summary>
    ///     Create new Peso
    /// </summary>
    /// <param name="parameters">Params to create a new Peso</param>
    /// <returns>The created peso or an Error</returns>
    Task<ErrorOr<Peso>> Create(CreatePesoParams parameters);

    /// <summary>
    ///     Find all Pesos
    /// </summary>
    /// <param name="idAnimal">Id of the animal to find the pesos</param>
    /// <returns>All the pesos of the animal</returns>
    Task<IEnumerable<Peso>> FindAll(int idAnimal);
}

public class CreatePesoParams
{
    public int IdAnimal { get; set; }
    public decimal PesoActual { get; set; }
    public DateOnly Fecha { get; set; }
}