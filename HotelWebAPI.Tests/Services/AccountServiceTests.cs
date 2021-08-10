using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;
using Moq;
using HotelWebAPI.Services;
using AutoMapper;
using HotelWebAPI.Models.Dtos;
using HotelWebAPI.Tests.Services.Seeders;
using Microsoft.AspNetCore.Identity;
using HotelWebAPI.Entities;
using Microsoft.Extensions.Logging;
using HotelWebAPI.Repositories;

namespace HotelWebAPI.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IPasswordHasher<User>> _passwordHasher = new Mock<IPasswordHasher<User>>();
        private readonly Mock<ILogger<AccountService>> _logger = new Mock<ILogger<AccountService>>();
        private readonly Mock<AuthenticationSettings> _authenticationSetting = new Mock<AuthenticationSettings>();
        private readonly AccountService _sut;

        public AccountServiceTests()
        {
            _sut = new AccountService(_userService.Object, _userRepository.Object, _mapper.Object, _passwordHasher.Object, _logger.Object, _authenticationSetting.Object);
        }

        [Fact]
        public async Task Register_ReturnsRegisteredUserId()
        {
            _mapper.Setup(m => m.Map<CreateUserDto>(It.IsAny<RegisterUserDto>()))
                .Returns(UserSeeder.GetCreateUserDto());
            _userService.Setup(s => s.Create(It.IsAny<CreateUserDto>()))
                .ReturnsAsync(UserSeeder.GetUserDto().Id);
              
            var registerDto = new RegisterUserDto()
            {
                Email = "randomemail@local.com",
                Password = "RandomPassword",
                ConfirmPassword = "RandomPassword"
            };

            var result = await _sut.Register(registerDto);

            IsType<int>(result);
        }
    }
}
