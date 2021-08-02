using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Models.Dtos
{
    public class CreateHotelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }

        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}

