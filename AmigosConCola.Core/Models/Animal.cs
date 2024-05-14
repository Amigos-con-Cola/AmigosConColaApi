namespace AmigosConCola.Core.Models;

public sealed class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public AnimalGender Gender { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool Adopted { get; set; }
    public AnimalSpecies Species { get; set; }
}