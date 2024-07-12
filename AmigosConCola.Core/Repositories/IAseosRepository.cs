using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IAseosRepository
{
    /// <summary>
    ///     Create a new aseo.
    /// </summary>
    /// <param name="parameters">The params required to create an aseo.</param>
    /// <returns>The created Aseo or an error</returns>
    Task<ErrorOr<Aseo>> Create(CreateAseoParams parameters);
}