using HotelWebAPI.Entities;
using HotelWebAPI.Entities.ApiData;
using HotelWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWebAPI.Tests.Services.Seeders
{
    public static class UserSeeder
    {
        public static List<User> GetUsers()
        {
            return new List<User>{
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
        }

        public static User GetUser()
        {
            return new User()
            {
                Email = "GenericEmail3",
                PasswordHash = "Hash",
                Role = new Role()
                {
                    Name = "User"
                }

            };
        }

        
    }
}
