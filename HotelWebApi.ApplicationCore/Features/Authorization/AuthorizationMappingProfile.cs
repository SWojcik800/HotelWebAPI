using AutoMapper;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Dtos.Authorization;

namespace HotelWebApi.ApplicationCore.Features.Authorization
{
    public class AuthorizationMappingProfile : Profile
    {

        public AuthorizationMappingProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(u => u.RoleName, c => c.MapFrom(s => s.Role.Name));
                    CreateMap<Role, RoleDto>();
                    CreateMap<CreateRoleDto, Role>();
                    CreateMap<RegisterUserDto, CreateUserDto>();
        }
    }


}
