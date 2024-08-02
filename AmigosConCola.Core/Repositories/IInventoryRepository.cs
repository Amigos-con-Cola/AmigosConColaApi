using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.Core.Repositories;

public interface IInventoryRepository
{
    /// <summary>
    ///     Create a new inventory item.
    /// </summary>
    /// <param name="parameters">Parameters needed to create a new inventory item.</param>
    /// <returns>The created inventory item or an error.</returns>
    public Task<ErrorOr<InventoryItem>> CreateInventoryItem(CreateInventoryItemParams parameters);
}

public class CreateInventoryItemParams
{
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