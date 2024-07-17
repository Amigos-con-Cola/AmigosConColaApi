namespace AmigosConCola.Core.Models;

public sealed class Peso
{
    public int Id { get; set; }
    public int IdAnimal { get; set; }
    public decimal PesoActual { get; set; }
    public DateOnly Fecha { get; set; }
}