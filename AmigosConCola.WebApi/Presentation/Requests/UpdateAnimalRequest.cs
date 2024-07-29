using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Requests;

public class UpdateAnimalRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("description")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("species")]
    public string? Species { get; set; }

    [JsonPropertyName("story")]
    public string? Story { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("weight")]
    public decimal? Weight { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("adopted")]
    public bool? Adopted { get; set; }
}