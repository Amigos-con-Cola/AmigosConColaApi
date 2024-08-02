using AmigosConCola.Core.Repositories;

namespace AmigosConCola.Core.UseCases;

public class CountAllInventoryItemsUseCase
{
    private readonly IInventoryRepository _inventory;

    public CountAllInventoryItemsUseCase(IInventoryRepository inventory)
    {
        _inventory = inventory;
    }

    public async Task<int> Invoke()
    {
        return await _inventory.CountAllItems();
    }
}