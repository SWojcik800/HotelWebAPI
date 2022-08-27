using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Contracts.Dtos.Authorization
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
