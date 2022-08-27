using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HotelWebAPI.Tests.Repositories
{
    public class HotelAPIDbTest : IDisposable
    {
        protected readonly HotelWebAPIDbContext context;
        
        public HotelAPIDbTest()
        {
            var options = new DbContextOptionsBuilder<HotelWebAPIDbContext>()
                .UseSqlite(CreateInMemoryDatabase()).Options;

            context = new HotelWebAPIDbContext(options);
            context.Database.EnsureCreated();

            var hotelsToSeed = new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "hotel 1",
                    Description = "hotel 1 desc",
                    Stars = 3,
                    ContactEmail = "mock@email.com",
                    PhoneNumber = "192 168 001",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        
                        Street = "Generic street",
                        ZipCode = "34-777"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 2",
                    Description = "hotel 2 desc",
                    Stars = 4,
                    ContactEmail = "mock2@email.com",
                    PhoneNumber = "192 168 002",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                       
                        Street = "Generic street",
                        ZipCode = "34-778"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 3",
                    Description = "hotel 3 desc",
                    Stars = 5,
                    ContactEmail = "mock3@email.com",
                    PhoneNumber = "192 168 003",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                       
                        Street = "Generic street",
                        ZipCode = "34-779"
                    }
                },
            };

            context.AddRange(hotelsToSeed);
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
