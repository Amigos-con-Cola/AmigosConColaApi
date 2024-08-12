namespace AmigosConCola.Core.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string MainIngredient { get; set; }
    public string? Format { get; set; }
    public required int Volume { get; set; }
    public required string Via { get; set; }
    public required DateOnly ExpirationDate { get; set; }
    public string? Laboratory { get; set; }
    public string? Origin { get; set; }
    public required string Status { get; set; }
    public required DateOnly EntryDate { get; set; }
    public required string Location { get; set; }
    public required string Kind { get; set; }
}