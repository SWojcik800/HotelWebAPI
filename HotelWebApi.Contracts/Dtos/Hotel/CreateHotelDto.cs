using System;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Contracts.Dtos
{
    public class CreateHotelDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(1, 6)]
        public int Stars { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Required]
        [Phone]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        public string City { get; set; }

        [Required]
        [MaxLength(40)]
        public string Street { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }
    }
}

