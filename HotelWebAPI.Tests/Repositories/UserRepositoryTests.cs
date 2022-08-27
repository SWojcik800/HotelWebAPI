using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.ApplicationCore.Repositories;
using HotelWebAPI.Tests.Services.Seeders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Repositories
{
    public class UserRepositoryTests : UserAPIDbTest
    {
        private readonly UserRepository _sut;
        
        

        public UserRepositoryTests() : base()
        {
           
            _sut = new UserRepository(context);

            
        }

       

        [Fact]
        public async Task GetAll_ReturnsListOfUsers()
        {

            var result = await _sut.GetAll();

            IsType<List<User>>(result);
        }

        [Fact]
        public async Task GetById_ReturnsUser_IfExists()
        {
            var users = await _sut.GetAll();
            var existingUserId = users[0].Id;
            var result = await _sut.GetById(existingUserId);

            IsType<User>(result);
            NotNull(result.Email);
            NotNull(result.Role.Name);
        }

        [Fact]
        public async Task GetById_ReturnsNull_IfUserDoesNotExist()
        {
            var nonExistentUserId = 9999;
            var result = await _sut.GetById(nonExistentUserId);
            Null(result);

        }

        [Fact]
        public async Task Create_ReturnsCreatedUser()
        {
            var user = UserSeeder.GetUser();
            var result = await _sut.Create(user);

            IsType<User>(result);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedUser_IfExists()
        {
            var userId = context.Users.First().Id;
            var result = await _sut.Delete(userId);

            IsType<User>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNull_IfUserDoesNotExist()
        {
            var nonExistentUserId = 9999;
            var result = await _sut.Delete(nonExistentUserId);

            Null(result);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedUser_IfUserExists()
        {
            var user = context.Users.First();
            user.Email = "updated@localhost.com";
            var result = await _sut.Update(user.Id, user);

            IsType<User>(result);
            Equal("updated@localhost.com", result.Email);
        }

        [Fact]
        public async Task Update_ReturnsNull_IfUserDoesNotExists()
        {
            var nonExistentUserId = 9999;
            var user = context.Users.First();
            var result = await _sut.Update(nonExistentUserId, user);

            Null(result);
        }

        [Fact]
        public async Task Update_ReturnsNull_IfUserIdDiffersFromIdParameter()
        {
            var differentUserId = context.Users.First().Id + 1;
            var user = context.Users.First();
            var result = await _sut.Update(differentUserId, user);

            Null(result);
        }





    }
}
