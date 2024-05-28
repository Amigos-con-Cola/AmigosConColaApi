using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class CreateAnimalRequest
{
    [JsonPropertyName("nombre")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("edad")]
    public int Age { get; set; }

    [JsonPropertyName("genero")]
    public string Gender { get; set; } = null!;

    [JsonPropertyName("imagen")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("especie")]
    public string Species { get; set; } = null!;

    [JsonPropertyName("historia")]
    public string? Story { get; set; }

    [JsonPropertyName("ubicacion")]
    public string? Location { get; set; }

    [JsonPropertyName("peso")]
    public decimal Weight { get; set; }

    [JsonPropertyName("codigo")]
    public string? Code { get; set; }
}