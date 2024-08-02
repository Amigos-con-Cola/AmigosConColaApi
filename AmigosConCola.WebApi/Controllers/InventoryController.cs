using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Throw;

namespace AmigosConCola.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InventoryController : BaseApiController
{
    private readonly CountAllInventoryItemsUseCase _countAllInventoryItems;
    private readonly CreateInventoryItemUseCase _createInventoryItem;
    private readonly GetPaginatedInventoryItems _getPaginatedInventoryItems;
    private readonly IMapper _mapper;

    public InventoryController(IMapper mapper, CreateInventoryItemUseCase createInventoryItem,
        GetPaginatedInventoryItems getPaginatedInventoryItems, CountAllInventoryItemsUseCase countAllInventoryItems)
    {
        _mapper = mapper;
        _createInventoryItem = createInventoryItem;
        _getPaginatedInventoryItems = getPaginatedInventoryItems;
        _countAllInventoryItems = countAllInventoryItems;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        [FromQuery]
        int page = 1,
        [FromQuery]
        int perPage = 10)
    {
        var paginationParams = new PaginationParams(page, perPage);
        var result = await _getPaginatedInventoryItems.Invoke(paginationParams);
        result.IsError.Throw("Failed to retrieve inventory items").IfTrue();

        var itemResponses = result.Value.Select(x => _mapper.Map<InventoryItemResponse>(x));
        var count = await _countAllInventoryItems.Invoke();

        var response = new PaginatedDataResponse<InventoryItemResponse>
        {
            Data = itemResponses,
            NextPage = page + 1,
            TotalItems = count,
            TotalPages = (int)Math.Ceiling((float)count / perPage)
        };

        return Ok(response);
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