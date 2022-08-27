using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelWebApi.ApplicationCore.Common.Seeders
{
    public class HotelSeeder
    {
        private readonly HotelWebAPIDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public HotelSeeder(HotelWebAPIDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {

            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();

                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any())
                {
                    _dbContext.AddRange(GetRoles());
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Hotels.Any())
                {
                    _dbContext.AddRange(GetHotels());
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    _dbContext.AddRange(GetUsers());
                    _dbContext.SaveChanges();
                }

            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Moderator"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }

        private IEnumerable<User> GetUsers()
        {

            var users = new List<User>()
            {

                new User()
                {
                    Email = "user@localhost.com",
                    RoleId = 1,

                },
                new User()
                {
                    Email = "moderator@localhost.com",
                    RoleId = 2,

                },
                new User()
                {
                    Email = "admin@localhost.com",
                    RoleId = 3,

                }
            };

            foreach (var user in users)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, "Password1!");
            }

            return users;
        }


        private IEnumerable<Hotel> GetHotels()
        {
            var hotels = new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "Hotel 1",
                    Description = "Hotel 1 desc",
                    Stars = 1,
                    ContactEmail = "hotel1@email.com",
                    PhoneNumber = "192-168-001",
                    Address = new Address()
                    {
                        City = "Hotel 1 city",
                        Street = "Hotel 1 street",
                        ZipCode = "12-345"
                    }

                },
                 new Hotel()
                {
                    Name = "Hotel 2",
                    Description = "Hotel 2 desc",
                    Stars = 2,
                    ContactEmail = "hotel2@email.com",
                    PhoneNumber = "192-168-002",
                    Address = new Address()
                    {
                        City = "Hotel 2 city",
                        Street = "Hotel 2 street",
                        ZipCode = "12-345"
                    }

                },
                  new Hotel()
                {
                    Name = "Hotel 3",
                    Description = "Hotel 3 desc",
                    Stars = 3,
                    ContactEmail = "hotel3@email.com",
                    PhoneNumber = "192-168-003",
                    Address = new Address()
                    {
                        City = "Hotel 3 city",
                        Street = "Hotel 3 street",
                        ZipCode = "12-345"
                    }

                }
            };

            return hotels;
        }
    }
}
