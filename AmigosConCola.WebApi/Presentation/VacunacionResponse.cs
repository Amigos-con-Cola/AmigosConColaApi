using System.Text.Json.Serialization;
using AmigosConCola.Core.Models;

namespace AmigosConCola.WebApi.Presentation;

public class VacunacionResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("id_animal")]
    public int IdAnimal { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    public static VacunacionResponse FromDomain(Vacunacion vacunacion)
    {
        return new VacunacionResponse
        {
            Id = vacunacion.Id ?? 0,
            IdAnimal = vacunacion.IdAnimal,
            Name = vacunacion.Name,
            Date = vacunacion.Date
        };
    }
}