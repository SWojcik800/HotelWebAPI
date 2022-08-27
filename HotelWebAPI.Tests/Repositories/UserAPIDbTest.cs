using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HotelWebAPI.Tests.Repositories
{
    public class UserAPIDbTest : IDisposable
    {
        protected readonly HotelWebAPIDbContext context;
        
        public UserAPIDbTest()
        {
            var options = new DbContextOptionsBuilder<HotelWebAPIDbContext>()
                .UseSqlite(CreateInMemoryDatabase()).Options;

            context = new HotelWebAPIDbContext(options);
            context.Database.EnsureCreated();

            var usersToSeed = new List<User>{
                new User()
                {
                    Email = "GenericEmail1",
                    PasswordHash = "Hash",
                    Role = new Role()
                    {
                        Name="Admin"
                    }

                },
                new User()
                {
                    Email = "GenericEmail2",
                    PasswordHash = "Hash",
                    Role = new Role()
                    {
                        Name="Moderator"
                    }

                },
                new User()
                {
                    Email = "GenericEmail3",
                    PasswordHash = "Hash",
                    Role = new Role()
                    {
                        Name="User"
                    }

                },
            };

            context.AddRange(usersToSeed);
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
