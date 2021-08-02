using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Dtos
{
    public class UpdateHotelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
    }
}
