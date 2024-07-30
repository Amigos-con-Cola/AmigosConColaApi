using System.ComponentModel.DataAnnotations.Schema;
using AmigosConCola.Core.Models;

namespace AmigosConCola.WebApi.Data.Entities;

[Table("vacunaciones")]
public class VacunacionEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("examen_previo")]
    public string? ExamenPrevio { get; set; }

    public AnimalEntity Animal { get; set; } = null!;

    public Vacunacion ToDomain()
    {
        return new Vacunacion
        {
            Id = Id,
            IdAnimal = IdAnimal,
            Name = Name,
            Date = Date,
            ExamenPrevio = ExamenPrevio
        };
    }
}