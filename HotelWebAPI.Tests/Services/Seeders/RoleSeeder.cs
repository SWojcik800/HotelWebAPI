using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Dtos.Authorization;
using System.Collections.Generic;

namespace HotelWebAPI.Tests.Services.Seeders
{
    public static class RoleSeeder
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Moderator"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
        }

        public static List<RoleDto> GetRoleDtos()
        {
            return new List<RoleDto>()
            {
                new RoleDto()
                {
                    Name = "User"
                },
                new RoleDto()
                {
                    Name = "Moderator"
                },
                new RoleDto()
                {
                    Name = "Admin"
                }
            };
        }

        public static RoleDto GetRoleDto()
        {
            return new RoleDto()
            {
                Name = "User"
            };
        }

        public static Role GetRole()
        {
            return new Role()
            {
                Name = "User"
            };
        }
    }
}
