using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class GetPaginatedInventoryItemsUseCase
{
    private readonly IInventoryRepository _inventory;

    public GetPaginatedInventoryItemsUseCase(IInventoryRepository inventory)
    {
        _inventory = inventory;
    }

    public async Task<ErrorOr<IEnumerable<InventoryItem>>> Invoke(PaginationParams paginationParams)
    {
        return await _inventory.GetPaginated(paginationParams);
    }
}