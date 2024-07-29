using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Dto;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperAseosProfile : Profile
{
    public AutoMapperAseosProfile()
    {
        // Aseos
        CreateMap<Aseo, AseoResponse>();
        CreateMap<AseoDto, Aseo>();
        CreateMap<CreateAseoParams, AseoDto>();
    }
}