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
                    Email = "GenericEmail1@localhost.com",
                    PasswordHash = "Hash",
                    Role = new Role()
                    {
                        Name="Admin"
                    }

                },
                new User()
                {
                    Email = "GenericEmail2@localhost.com",
                    PasswordHash = "Hash",
                    Role = new Role()
                    {
                        Name="Moderator"
                    }

                },
                new User()
                {
                    Email = "GenericEmail3@localhost.com",
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
                Email = "GenericEmail3@localhost.com",
                PasswordHash = "Hash",
                Role = new Role()
                {
                    Name = "User"
                }

            };
        }

        public static List<UserDto> GetUserDtos()
        {
            return new List<UserDto>{
                new UserDto()
                {
                    Email = "GenericEmail1@localhost.com",
                    PasswordHash = "Hash",
                    RoleName="Admin"
                    

                },
                new UserDto()
                {
                    Email = "GenericEmail2@localhost.com",
                    PasswordHash = "Hash",
                    RoleName="Moderator"


                },
                new UserDto()
                {
                    Email = "GenericEmail3@localhost.com",
                    PasswordHash = "Hash",
                    RoleName="User"


                }
            };
        }

        public static UserDto GetUserDto()
        {
            return new UserDto()
            {
                Email = "GenericEmail3@localhost.com",
                PasswordHash = "Hash",
                RoleName = "User"


            };
        }

        public static CreateUserDto GetCreateUserDto()
        {
            return new CreateUserDto()
            {
                Email = "GenericEmail3@localhost.com",
                Password = "Password",
                ConfirmPassword = "Password",
                RoleId = 1
            };
        }

        public static UpdateUserDto GetUpdateUserDto()
        {
            return new UpdateUserDto()
            {
                NewPassword = "NewPassword",
                ConfirmNewPassword = "NewPassword"
            };
        }


    }
}
