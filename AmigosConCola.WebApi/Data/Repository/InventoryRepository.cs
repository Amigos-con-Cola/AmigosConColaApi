using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Entities;
using AutoMapper;
using ErrorOr;

namespace AmigosConCola.WebApi.Data.Repository;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public InventoryRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ErrorOr<InventoryItem>> CreateInventoryItem(CreateInventoryItemParams parameters)
    {
        try
        {
            var entity = _mapper.Map<InventoryItemEntity>(parameters);
            var result = await _db.Inventory.AddAsync(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<InventoryItem>(result.Entity);
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: $"Failed to create inventory item: {ex}");
        }
    }
}