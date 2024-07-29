using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Requests;

public class CreateAseoRequest
{
    [JsonPropertyName("tipo")]
    public string Tipo { get; set; } = null!;

    [JsonPropertyName("fecha")]
    public DateOnly Fecha { get; set; }
}