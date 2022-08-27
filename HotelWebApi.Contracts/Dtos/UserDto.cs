using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Contracts.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(30)]
        public string RoleName { get; set; }
    }
}
