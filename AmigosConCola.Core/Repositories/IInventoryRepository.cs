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

    /// <summary>
    ///     Get a paginated list of inventory items.
    /// </summary>
    /// <returns>A list of paginated inventory items or an error.</returns>
    public Task<ErrorOr<IEnumerable<InventoryItem>>> GetPaginated(PaginationParams paginationParams);

    /// <summary>
    ///     Get a count of all the items in the inventory.
    /// </summary>
    /// <returns>The count of items in the inventory.</returns>
    public Task<int> CountAllItems();
}

public class CreateInventoryItemParams
{
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