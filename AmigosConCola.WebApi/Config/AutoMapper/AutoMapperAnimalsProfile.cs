using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Entities;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using AutoMapper;

namespace AmigosConCola.WebApi.Config.AutoMapper;

public class AutoMapperAnimalsProfile : Profile
{
    public AutoMapperAnimalsProfile()
    {
        CreateMap<AnimalEntity, Animal>();
        CreateMap<UpdateAnimalRequest, UpdateAnimalParams>();
        CreateMap<Animal, AnimalResponse>();
    }
}