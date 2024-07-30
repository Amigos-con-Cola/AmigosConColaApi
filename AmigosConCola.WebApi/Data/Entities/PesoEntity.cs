using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Entities;

[Table("pesos")]
public sealed class PesoEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("peso_actual")]
    public decimal PesoActual { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    public AnimalEntity Animal { get; set; } = null!;
}