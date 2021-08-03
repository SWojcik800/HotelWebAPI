using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;
using Moq;
using HotelWebAPI.Repositories;
using HotelWebAPI.Tests.Services.Seeders;
using HotelWebAPI.Models.Dtos;
using AutoMapper;
using HotelWebAPI.Entities;
using HotelWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using HotelWebAPI.Exceptions;

namespace HotelWebAPI.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IPasswordHasher<User>> _passwordHasher = new Mock<IPasswordHasher<User>>();

        private readonly UserService _sut;

        public UserServiceTest()
        {
            _sut = new UserService(_userRepository.Object, _mapper.Object, _passwordHasher.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsers()
        {
            _userRepository
                .Setup(u => u.GetAll())
                .ReturnsAsync(UserSeeder.GetUsers());

            _mapper
                .Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>()))
                .Returns(UserSeeder.GetUserDtos());

            var result = await _sut.GetAll();

            IsType<List<UserDto>>(result);
        }

        [Fact]
        public async Task GetById_ReturnsUserDto_IfUserExists()
        {
            _userRepository
                .Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(UserSeeder.GetUser());

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            var existingUserId = UserSeeder.GetUser().Id;
            var result = await _sut.GetById(existingUserId);

            IsType<UserDto>(result);
        }

        [Fact]
        public async Task GetById_ReturnsNull_IfUserDoesNotExist()
        {
            _userRepository
                .Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            var existingUserId = UserSeeder.GetUser().Id;
            var result = await _sut.GetById(existingUserId);

            Null(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedUserId()
        {
            _userRepository
                .Setup(u => u.Create(It.IsAny<User>()))
                .ReturnsAsync(UserSeeder.GetUser());

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            _passwordHasher
                .Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("HashedPassword");

            var dto = UserSeeder.GetCreateUserDto();
            var result = await _sut.Create(dto);

            IsType<int>(result);
            

        }
        [Fact]
        public async Task Create_ThrowsBadRequestException_IfPasswordsDiffer()
        {
            _userRepository
                .Setup(u => u.Create(It.IsAny<User>()))
                .ReturnsAsync(UserSeeder.GetUser());

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            _passwordHasher
                .Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("HashedPassword");

            var dto = UserSeeder.GetCreateUserDto();
            dto.ConfirmPassword = "RandomPassword";

            Func<Task> act = () => _sut.Create(dto);

            await ThrowsAsync<BadRequestException>(act);


        }

        [Fact]
        public async Task Delete_ReturnsDeletedUserDto_IfUserExisted()
        {
            _userRepository
                .Setup(u => u.Delete(It.IsAny<int>()))
                .ReturnsAsync(UserSeeder.GetUser());

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            var user = UserSeeder.GetUser();

            var result = await _sut.Delete(user.Id);

            IsType<UserDto>(result);

        }

        [Fact]
        public async Task Delete_ThrowsNotFoundException_IfUserDoesNotExist()
        {
            _userRepository
                .Setup(u => u.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            var user = UserSeeder.GetUser();
            Func<Task> act = () =>  _sut.Delete(user.Id);

            await ThrowsAsync<NotFoundException>(act);

        }

        [Fact]
        public async Task Update_ReturnsUpdatedUserDto_IfExists()
        {
            var mockUser = UserSeeder.GetUser();
            
            _userRepository
                .Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(mockUser);

            _userRepository
                .Setup(u => u.Update(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(mockUser);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            _passwordHasher
                .Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("HashedPassword");

            var userToUpdate = UserSeeder.GetUpdateUserDto();
            var userId = UserSeeder.GetUser().Id;
            var result = await _sut.Update(userId, userToUpdate);

            IsType<UserDto>(result);
        }

        [Fact]
        public async Task Update_ThrowsNotFoundException_IfUserDoesNotExist()
        {
            var mockUser = UserSeeder.GetUser();

            _userRepository
                .Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            _userRepository
                .Setup(u => u.Update(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(mockUser);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            _passwordHasher
                .Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("HashedPassword");

            var userToUpdate = UserSeeder.GetUpdateUserDto();
            var userId = UserSeeder.GetUser().Id;
            

            Func<Task> act = () => _sut.Update(userId, userToUpdate);
            await ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task Update_ThrowsBadRequestException_WhenUserDoesNotExist()
        {
            var mockUser = UserSeeder.GetUser();

            _userRepository
                .Setup(u => u.GetById(It.IsAny<int>()))
                .ReturnsAsync(mockUser);

            _userRepository
                .Setup(u => u.Update(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(mockUser);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
                .Returns(UserSeeder.GetUserDto());

            _passwordHasher
                .Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("HashedPassword");

            var userToUpdate = UserSeeder.GetUpdateUserDto();
            var userId = UserSeeder.GetUser().Id;

            userToUpdate.ConfirmNewPassword = "RandomPassword";

            Func<Task> act = () => _sut.Update(userId, userToUpdate);

            await ThrowsAsync<BadRequestException>(act);
        }



    }
}
