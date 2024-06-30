using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Presentation;

public class CreateAnimalRequest
{
    [BindProperty(Name = "nombre")]
    [JsonPropertyName("nombre")]
    public string Name { get; set; } = null!;

    [BindProperty(Name = "edad")]
    [JsonPropertyName("edad")]
    public int Age { get; set; }

    [BindProperty(Name = "genero")]
    [JsonPropertyName("genero")]
    public string Gender { get; set; } = null!;

    [BindProperty(Name = "imagen")]
    [JsonPropertyName("imagen")]
    public IFormFile? Image { get; set; }

    [BindProperty(Name = "especie")]
    [JsonPropertyName("especie")]
    public string Species { get; set; } = null!;

    [BindProperty(Name = "historia")]
    [JsonPropertyName("historia")]
    public string? Story { get; set; }

    [BindProperty(Name = "edad")]
    [JsonPropertyName("ubicacion")]
    public string? Location { get; set; }

    [BindProperty(Name = "peso")]
    [JsonPropertyName("peso")]
    public decimal Weight { get; set; }

    [BindProperty(Name = "codigo")]
    [JsonPropertyName("codigo")]
    public string? Code { get; set; }
}
