using System.ComponentModel.DataAnnotations.Schema;
using AmigosConCola.Core.Models;

namespace AmigosConCola.WebApi.Data.Dto;

[Table("vacunaciones")]
public class VacunacionDto
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("date")]
    public DateOnly Date { get; set; }

    public AnimalDto Animal { get; set; } = null!;

    public Vacunacion ToDomain()
    {
        return new Vacunacion
        {
            Id = Id,
            IdAnimal = IdAnimal,
            Name = Name,
            Date = Date
        };
    }
}