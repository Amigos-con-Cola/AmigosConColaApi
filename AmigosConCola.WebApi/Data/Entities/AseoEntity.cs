using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Entities;

[Table("aseos")]
public sealed class AseoEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = null!;

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    public AnimalEntity Animal { get; set; } = null!;
}