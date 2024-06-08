namespace AmigosConCola.Core.Repositories;

public class CreateVacunacionParams
{
    public int IdAnimal { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? ExamenPrevio { get; set; }
}