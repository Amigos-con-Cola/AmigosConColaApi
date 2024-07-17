namespace AmigosConCola.Core.Repositories;

public class CreatePesoParams
{
    public int IdAnimal { get; set; }
    public decimal PesoActual { get; set; }
    public DateOnly Fecha { get; set; }
}