using HotelWebApi.Contracts.Dtos.Authorization;
using HotelWebApi.Contracts.Services;
using HotelWebAPI.Controllers;
using HotelWebAPI.Tests.Services.Seeders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleService> _roleService = new Mock<IRoleService>();
        private RoleController _sut;

        public RoleControllerTests()
        {
            _sut = new RoleController(_roleService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllRoleDtos()
        {
            _roleService.Setup(r => r.GetAll())
                .ReturnsAsync(RoleSeeder.GetRoleDtos());

            var result = await _sut.GetAll();

            IsType<ActionResult<List<RoleDto>>>(result);
            NotNull(result);
        }

        [Fact]
        public async Task GetById_ReturnsRoleDto_IfExists()
        {
            _roleService.Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(RoleSeeder.GetRoleDto());

            var result = await _sut.GetById(RoleSeeder.GetRole().Id);
            IsType<ActionResult<RoleDto>>(result);
            NotNull(result);
        }

        [Fact]

        public async Task Create_ReturnsCreatedAtResult()
        {
            _roleService.Setup(r => r.Create(It.IsAny<CreateRoleDto>()))
                .ReturnsAsync(RoleSeeder.GetRoleDto());

            var dto = new CreateRoleDto()
            {
                Name = "NewRoleName"
            };
            var result = await _sut.Create(dto);
            IsType<CreatedResult>(result);
            
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            var roleId = RoleSeeder.GetRole().Id;
            _roleService.Setup(r => r.Delete(It.IsAny<int>()))
                .ReturnsAsync(RoleSeeder.GetRoleDto());

            var result = await _sut.Delete(roleId);
            IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult()
        {

            var roleId = RoleSeeder.GetRole().Id;
            var dto = new CreateRoleDto()
            {
                Name = "UpdatedRoleName"
            };
            _roleService.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<CreateRoleDto>()))
                .ReturnsAsync(RoleSeeder.GetRoleDto());

            var result = await _sut.Update(roleId, dto);
            IsType<OkObjectResult>(result);
        }
    }
}
