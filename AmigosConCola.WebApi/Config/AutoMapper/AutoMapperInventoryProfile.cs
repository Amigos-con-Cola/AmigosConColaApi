using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Entities;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperInventoryProfile : Profile
{
    public AutoMapperInventoryProfile()
    {
        CreateMap<CreateInventoryItemRequest, CreateInventoryItemParams>();
        CreateMap<CreateInventoryItemParams, InventoryItemEntity>();
        CreateMap<InventoryItemEntity, InventoryItem>();
        CreateMap<InventoryItem, InventoryItemResponse>();
    }
}