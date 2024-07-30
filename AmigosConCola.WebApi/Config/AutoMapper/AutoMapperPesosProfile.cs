using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Entities;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperPesosProfile : Profile
{
    public AutoMapperPesosProfile()
    {
        CreateMap<Peso, PesoResponse>();
        CreateMap<PesoEntity, Peso>();
        CreateMap<CreatePesoParams, PesoEntity>();
    }
}