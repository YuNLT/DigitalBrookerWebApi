using AutoMapper;
using DigitalBroker.Application.DTOs;
using DigitalBrooker.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
