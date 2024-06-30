namespace AmigosConCola.Core.Models;

public class Desparasitacion
{
    public int Id { get; set; }

    public int IdAnimal { get; set; }

    public string Tipo { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Producto { get; set; } = null!;

    public int Peso { get; set; }

    public string Formato { get; set; } = null!;
}
