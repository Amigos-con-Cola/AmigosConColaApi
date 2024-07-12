namespace AmigosConCola.Core.Repositories;

public class CreateAseoParams
{
    public int IdAnimal { get; set; }
    public string Tipo { get; set; } = null!;
    public DateOnly Fecha { get; set; }
}