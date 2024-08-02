using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public class CreateInventoryItemUseCase
{
    private readonly IInventoryRepository _inventory;

    public CreateInventoryItemUseCase(IInventoryRepository inventory)
    {
        _inventory = inventory;
    }

    public async Task<ErrorOr<InventoryItem>> Invoke(CreateInventoryItemParams parameters)
    {
        // TODO: Handle parameters validation.
        return await _inventory.CreateInventoryItem(parameters);
    }
}