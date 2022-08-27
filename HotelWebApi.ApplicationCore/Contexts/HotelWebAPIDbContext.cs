using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApi.ApplicationCore.Contexts
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
