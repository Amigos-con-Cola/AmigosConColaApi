using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Requests;

public class UpdateAnimalRequest
{
    [JsonPropertyName("nombre")]
    public string? Name { get; set; }

    [JsonPropertyName("edad")]
    public int? Age { get; set; }

    [JsonPropertyName("genero")]
    public string? Gender { get; set; }

    [JsonPropertyName("descripcion")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("especie")]
    public string? Species { get; set; }

    [JsonPropertyName("historia")]
    public string? Story { get; set; }

    [JsonPropertyName("ubicacion")]
    public string? Location { get; set; }

    [JsonPropertyName("peso")]
    public decimal? Weight { get; set; }

    [JsonPropertyName("codigo")]
    public string? Code { get; set; }

    [JsonPropertyName("adoptado")]
    public bool? Adopted { get; set; }
}