using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Presentation;

public class CreateAnimalRequest
{
    [BindProperty(Name = "nombre")]
    public string Name { get; set; } = null!;

    [BindProperty(Name = "edad")]
    public int Age { get; set; }

    [BindProperty(Name = "genero")]
    public string Gender { get; set; } = null!;

    [BindProperty(Name = "imagen")]
    public IFormFile? Image { get; set; }

    [BindProperty(Name = "especie")]
    public string Species { get; set; } = null!;

    [BindProperty(Name = "historia")]
    public string? Story { get; set; }

    [BindProperty(Name = "ubicacion")]
    public string Location { get; set; } = null!;

    [BindProperty(Name = "peso")]
    public decimal Weight { get; set; }

    [BindProperty(Name = "codigo")]
    public string? Code { get; set; }
}
