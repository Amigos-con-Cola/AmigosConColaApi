using System.ComponentModel.DataAnnotations.Schema;

namespace AmigosConCola.WebApi.Data.Entities;

[Table("inventory")]
public class InventoryItemEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("main_ingredient")]
    public required string MainIngredient { get; set; }

    [Column("format")]
    public string? Format { get; set; }

    [Column("volume")]
    public required int Volume { get; set; }

    [Column("via")]
    public required string Via { get; set; }

    [Column("expiration_date")]
    public required DateOnly ExpirationDate { get; set; }

    [Column("laboratory")]
    public string? Laboratory { get; set; }

    [Column("origin")]
    public string? Origin { get; set; }

    [Column("status")]
    public required string Status { get; set; }

    [Column("entry_date")]
    public required DateOnly EntryDate { get; set; }

    [Column("location")]
    public required string Location { get; set; }

    [Column("kind")]
    public required string Kind { get; set; }
}