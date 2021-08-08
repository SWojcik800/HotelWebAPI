using HotelWebAPI.Entities;
using HotelWebAPI.Entities.ApiData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWebAPI.Tests.Repositories
{
    public class RoleAPIDbTest : IDisposable
    {
        protected readonly HotelWebAPIDbContext context;

        public RoleAPIDbTest()
        {
            var options = new DbContextOptionsBuilder<HotelWebAPIDbContext>()
                .UseSqlite(CreateInMemoryDatabase()).Options;

            context = new HotelWebAPIDbContext(options);
            context.Database.EnsureCreated();

            var rolesToSeed = new List<Role>
            {

                new Role()
                {
                    Name = "Admin"
                },

                new Role()
                {
                    Name = "Moderator"
                },

                new Role()
                {
                    Name = "User"
                }


            };

            context.AddRange(rolesToSeed);
            context.SaveChanges();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
