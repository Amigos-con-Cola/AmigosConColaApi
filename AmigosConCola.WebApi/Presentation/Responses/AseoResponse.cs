using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Responses;

public class AseoResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("id_animal")]
    public int IdAnimal { get; set; }

    [JsonPropertyName("tipo")]
    public string Tipo { get; set; } = null!;

    [JsonPropertyName("fecha")]
    public DateOnly Fecha { get; set; }
}