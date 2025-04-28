using AutoMapper;
using DigitalBroker.Application.DTOs;
using DigitalBrooker.Domain.Entities.Models;
namespace DigitalBroker.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<User, RegisterRequest>().ReverseMap(); //(TSource, TDestination)
        }
    }
}
