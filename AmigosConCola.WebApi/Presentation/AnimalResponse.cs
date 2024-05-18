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

    [JsonPropertyName("historia")]
    public string? Story { get; set; }

    [JsonPropertyName("ubicacion")]
    public string? Location { get; set; }

    [JsonPropertyName("peso")]
    public decimal Weight { get; set; }

    [JsonPropertyName("codigo")]
    public string? Code { get; set; }

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
            Adopted = animal.Adopted,
            Story = animal.Story,
            Location = animal.Location,
            Weight = animal.Weight,
            Code = animal.Code
        };
    }
}