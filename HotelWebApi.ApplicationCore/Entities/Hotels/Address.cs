using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Entities.ApiData
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }


        public string ZipCode { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
