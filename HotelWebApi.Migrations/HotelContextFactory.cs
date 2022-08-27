using HotelWebApi.ApplicationCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace HotelWebApi.Migrations
{
    public class HotelContextFactory : IDesignTimeDbContextFactory<HotelWebAPIDbContext>
    {
        public HotelWebAPIDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var configurationBuilder = new ConfigurationBuilder();
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()) + @"\HotelWebApi";

            configurationBuilder
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("HotelDbConnection");

            optionsBuilder.UseSqlServer(connectionString,
                options => options.MigrationsAssembly("HotelWebApi.Migrations"));


            return new HotelWebAPIDbContext(optionsBuilder.Options);
        }
    }
}
