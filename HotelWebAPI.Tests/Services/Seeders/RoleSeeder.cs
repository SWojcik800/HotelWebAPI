using HotelWebAPI.Entities;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
