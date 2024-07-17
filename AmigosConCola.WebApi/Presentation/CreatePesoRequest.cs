using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class CreatePesoRequest
{
    [JsonPropertyName("peso_actual")]
    public decimal PesoActual { get; set; }
    [JsonPropertyName("fecha")]
    public DateOnly Fecha { get; set; }
}