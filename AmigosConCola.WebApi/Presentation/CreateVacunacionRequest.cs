using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class CreateVacunacionRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }
}