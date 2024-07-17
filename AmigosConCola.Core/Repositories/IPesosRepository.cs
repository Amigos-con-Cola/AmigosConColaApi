using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IPesosRepository
{
    /// <summary>
    /// Create new Peso
    /// </summary>
    /// <param name="parameters">Params to create a new Peso</param>
    /// <returns>The created peso or an Error</returns>
    Task<ErrorOr<Peso>> Create(CreatePesoParams parameters);
}