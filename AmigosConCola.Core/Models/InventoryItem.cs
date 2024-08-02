namespace AmigosConCola.Core.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string MainIngredient { get; set; }
    public required string Format { get; set; }
    public required string Volume { get; set; }
    public required string Via { get; set; }
    public required DateOnly ExpirationDate { get; set; }
    public required string Laboratory { get; set; }
    public required string Origin { get; set; }
    public required string Status { get; set; }
    public required DateOnly EntryDate { get; set; }
    public required string BoxId { get; set; }
    public required string Stock { get; set; }
    public required string Kind { get; set; }
}