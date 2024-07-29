using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Responses;

public sealed class DesparasitacionResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("id_animal")]
    public int IdAnimal { get; set; }

    [JsonPropertyName("tipo")]
    public string Tipo { get; set; } = null!;

    [JsonPropertyName("fecha")]
    public DateOnly Fecha { get; set; }

    [JsonPropertyName("producto")]
    public string Producto { get; set; } = null!;

    [JsonPropertyName("peso")]
    public decimal Peso { get; set; }

    [JsonPropertyName("formato")]
    public string Formato { get; set; } = null!;

}
