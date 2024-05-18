using System.Text.Json.Serialization;
using AmigosConCola.Core.Models;

namespace AmigosConCola.WebApi.Presentation;

public class AnimalResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = null!;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = null!;

    [JsonPropertyName("adopted")]
    public bool Adopted { get; set; }

    [JsonPropertyName("species")]
    public string Species { get; set; } = null!;

    public static AnimalResponse FromDomain(Animal animal)
    {
        return new AnimalResponse
        {
            Id = animal.Id,
            Name = animal.Name,
            Age = animal.Age,
            Gender = animal.Gender.ToString(),
            Species = animal.Species.ToString(),
            ImageUrl = animal.ImageUrl,
            Adopted = animal.Adopted
        };
    }
}