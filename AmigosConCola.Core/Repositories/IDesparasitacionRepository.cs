using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public record CreateDesparasitacionParams(
    int IdAnimal,
    string Tipo,
    DateOnly Fecha,
    string Producto,
    decimal Peso,
    string Formato);


public interface IDesparasitacionRepository
{
    /// <summary>
    /// List all desparasitaciones.
    /// </summary>
    Task<IEnumerable<Desparasitacion>> FindAll(int animalId);

    /// <summary>
    /// Create a new desparasitacion.
    /// </summary>
    Task<ErrorOr<Desparasitacion>> Create(CreateDesparasitacionParams parameters);
}
