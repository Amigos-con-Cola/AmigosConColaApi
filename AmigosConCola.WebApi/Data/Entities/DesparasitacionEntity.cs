using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Entities;

[Table("desparasitaciones")]
public sealed class DesparasitacionEntity
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = null!;

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("producto")]
    public string Producto { get; set; } = null!;

    [Column("peso")]
    public decimal Peso { get; set; }

    [Column("formato")]
    public string Formato { get; set; } = null!;

    public AnimalEntity Animal { get; set; } = null!;
}