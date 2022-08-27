using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Services;
using HotelWebAPI.Controllers;
using HotelWebAPI.Tests.Services.Seeders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Xunit.Assert;


namespace HotelWebAPI.Tests.Controllers
{

    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly UserController _sut;
        public UserControllerTests()
        {
            _sut = new UserController(_userService.Object);
        }

        [Fact]
        public async Task Index_ReturnsListOfUserDtos()
        {
            _userService.Setup(u => u.GetAll())
                .ReturnsAsync(UserSeeder.GetUserDtos());

            var result = await _sut.Index();

            IsType<ActionResult<List<UserDto>>>(result);
            
            
        }

        
        [Fact]
        public async Task GetById_ReturnsUserDto()
        {
            _userService.Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(UserSeeder.GetUserDto());

            var id = UserSeeder.GetUser().Id;
            var result = await _sut.GetById(id);

            IsType<ActionResult<UserDto>>(result);
            NotNull(result);
        
        
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            _userService.Setup(u => u.Create(It.IsAny<CreateUserDto>()))
                .ReturnsAsync(UserSeeder.GetUser().Id);

            var userToCreate = UserSeeder.GetCreateUserDto();
            var result = await _sut.Create(userToCreate);

            IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            _userService.Setup(u => u.Update(It.IsAny<int>(), It.IsAny<UpdateUserDto>()))
                .ReturnsAsync(UserSeeder.GetUserDto());

            var userToUpdate = UserSeeder.GetUpdateUserDto();
            var id = UserSeeder.GetUser().Id;
            var result = await _sut.Update(id, userToUpdate);

            IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            _userService.Setup(u => u.Delete(It.IsAny<int>()))
                .ReturnsAsync(UserSeeder.GetUserDto());
            var id = UserSeeder.GetUser().Id;
            var result = await _sut.Delete(id);

            IsType<NoContentResult>(result);
        }
        
    }
}
