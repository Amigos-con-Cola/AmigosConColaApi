using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class CreateAnimalRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = null!;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = null!;

    [JsonPropertyName("species")]
    public string Species { get; set; } = null!;
}