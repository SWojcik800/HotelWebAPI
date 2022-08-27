using AutoMapper;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos;

namespace HotelWebApi.ApplicationCore.Mapping
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

        }
    }
}
