namespace AmigosConCola.Core.Models;

public sealed class Aseo
{
    public int Id { get; set; }
    public int IdAnimal { get; set; }
    public string Tipo { get; set; } = null!;
    public DateOnly Fecha { get; set; }
}