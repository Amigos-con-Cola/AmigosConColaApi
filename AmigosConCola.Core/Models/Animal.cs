namespace AmigosConCola.Core.Models;

public sealed class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public AnimalGender Gender { get; set; }
    public string? ImageUrl { get; set; }
    public bool Adopted { get; set; }
    public AnimalSpecies Species { get; set; }
    public string? Story { get; set; }
    public string? Location { get; set; }
    public decimal Weight { get; set; }
    public string? Code { get; set; }
}