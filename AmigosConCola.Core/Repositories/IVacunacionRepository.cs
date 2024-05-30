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
}