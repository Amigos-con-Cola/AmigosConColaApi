using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Entities;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperAseosProfile : Profile
{
    public AutoMapperAseosProfile()
    {
        // Aseos
        CreateMap<Aseo, AseoResponse>();
        CreateMap<AseoEntity, Aseo>();
        CreateMap<CreateAseoParams, AseoEntity>();
    }
}