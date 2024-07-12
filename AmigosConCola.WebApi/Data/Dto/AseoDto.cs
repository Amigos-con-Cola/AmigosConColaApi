using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Dto;

[Table("aseos")]
public sealed class AseoDto
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = null!;

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    public AnimalDto Animal { get; set; } = null!;
}