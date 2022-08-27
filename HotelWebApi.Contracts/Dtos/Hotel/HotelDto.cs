using System;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Contracts.Dtos
{
    public class HotelDto
    {
        public int Id { get; set; }

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
