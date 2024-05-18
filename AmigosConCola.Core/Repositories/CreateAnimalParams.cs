using AmigosConCola.Core.Models;

namespace AmigosConCola.Core.Repositories;

public sealed class CreateAnimalParams
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public AnimalGender Gender { get; set; }
    public string ImageUrl { get; set; } = null!;
    public AnimalSpecies Species { get; set; }
    public string? Story { get; set; }
    public string? Location { get; set; }
    public decimal Weight { get; set; }
    public string? Code { get; set; }
}