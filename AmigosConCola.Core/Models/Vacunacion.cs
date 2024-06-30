namespace AmigosConCola.Core.Models;

public class Vacunacion
{
    public int? Id { get; set; }

    public int IdAnimal { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? ExamenPrevio { get; set; }
}
