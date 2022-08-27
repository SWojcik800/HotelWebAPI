using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();
        private AccountController _sut;

        public AccountControllerTests()
        {
            _sut = new AccountController(_accountService.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult()
        {
            _accountService.Setup(a => a.Register(It.IsAny<RegisterUserDto>()))
                .ReturnsAsync(UserSeeder.GetUserDto().Id);

            var registerDto = new RegisterUserDto()
            {
                Email = "randomemail@local.com",
                Password = "RandomPassword",
                ConfirmPassword = "RandomPassword"
            };

            var result = await _sut.Register(registerDto);
            IsType<OkResult>(result);
        }



    }
}
