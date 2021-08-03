using AutoMapper;
using HotelWebAPI.Entities;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Hotel, HotelDto>()
                .ForMember(h => h.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(h => h.ZipCode, c => c.MapFrom(s => s.Address.ZipCode))
                .ForMember(h => h.Street, c => c.MapFrom(s => s.Address.Street));

            CreateMap<CreateHotelDto, Hotel>()
                .ForMember(h => h.Address, c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    ZipCode = dto.ZipCode
                }));

            CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, c => c.MapFrom(s => s.Role.Name));

            
        }
    }
}
