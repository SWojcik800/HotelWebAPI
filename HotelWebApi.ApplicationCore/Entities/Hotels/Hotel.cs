using HotelWebApi.ApplicationCore.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Entities.ApiData
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }

        public string ContactEmail { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int? CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}
