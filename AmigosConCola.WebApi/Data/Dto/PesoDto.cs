using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Dto;

public sealed class PesoDto
{
    [Column ("id")]
    public int Id { get; set; }
    [Column ("id_animal")]
    public int IdAnimal { get; set; }
    [Column ("peso_actual")]
    public decimal PesoActual { get; set; }
    [Column ("fecha")]
    public DateOnly Fecha { get; set; }
    
    public AnimalDto Animal { get; set; } = null!;
}