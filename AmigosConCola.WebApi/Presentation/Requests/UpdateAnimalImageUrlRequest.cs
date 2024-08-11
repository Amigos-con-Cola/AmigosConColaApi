using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Presentation.Requests;

public class UpdateAnimalImageUrlRequest
{
    [BindProperty(Name = "imagen")]
    public required IFormFile Image { get; set; }
}