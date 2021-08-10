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

namespace HotelWebAPI.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly AccountService _sut;

        public AccountServiceTests()
        {
            _sut = new AccountService(_userService.Object, _mapper.Object);
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
