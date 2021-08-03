using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
    }
}
