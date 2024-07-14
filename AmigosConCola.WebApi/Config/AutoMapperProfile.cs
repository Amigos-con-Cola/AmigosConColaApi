using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Dto;
using AmigosConCola.WebApi.Presentation;
using AutoMapper;

namespace AmigosConCola.WebApi.Config;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Aseos
        CreateMap<Aseo, AseoResponse>();
        CreateMap<AseoDto, Aseo>();
        CreateMap<CreateAseoParams, AseoDto>();
    }
}