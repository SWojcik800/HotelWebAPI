using HotelWebAPI.Entities.ApiData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebAPI.Entities
{
    public class HotelWebAPIDbContext : DbContext
    {
        public HotelWebAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
