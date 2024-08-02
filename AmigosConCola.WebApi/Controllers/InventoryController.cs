using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Throw;

namespace AmigosConCola.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : BaseApiController
{
    private readonly CreateInventoryItemUseCase _createInventoryItem;
    private readonly IMapper _mapper;

    public InventoryController(IMapper mapper, CreateInventoryItemUseCase createInventoryItem)
    {
        _mapper = mapper;
        _createInventoryItem = createInventoryItem;
    }

    [HttpPost]
    public async Task<IActionResult> Store(CreateInventoryItemRequest request)
    {
        var parameters = _mapper.Map<CreateInventoryItemParams>(request);
        var result = await _createInventoryItem.Invoke(parameters);

        result.IsError
            .Throw(() => new Exception($"Failed to create inventory item: {result.FirstError.Description}"))
            .IfTrue();

        var response = _mapper.Map<InventoryItemResponse>(result.Value);
        return Ok(response);
    }
}