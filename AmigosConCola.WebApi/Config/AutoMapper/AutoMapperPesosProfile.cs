using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Dto;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperPesosProfile: Profile
{
    public AutoMapperPesosProfile()
    {
        CreateMap<Peso, PesoResponse>();
        CreateMap<PesoDto, Peso>();
        CreateMap<CreatePesoParams, PesoDto>();
    }
}