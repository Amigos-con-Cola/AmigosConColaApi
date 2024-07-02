using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Dto;

[Table("desparasitaciones")]
public sealed class DesparasitacionDto
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

    public AnimalDto Animal { get; set; } = null!;
}
