using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Responses;

public class PesoResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("id_animal")]
    public int IdAnimal { get; set; }

    [JsonPropertyName("peso_actual")]
    public decimal PesoActual { get; set; }

    [JsonPropertyName("fecha")]
    public DateOnly Fecha { get; set; }
}