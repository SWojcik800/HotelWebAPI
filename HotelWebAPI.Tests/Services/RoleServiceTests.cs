using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using HotelWebAPI.Tests.Services.Seeders;
using HotelWebApi.Contracts.Repositories;
using HotelWebApi.ApplicationCore.Features.Authorization;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.ApplicationCore.Common.Exceptions;
using HotelWebApi.Contracts.Dtos.Authorization;

namespace HotelWebAPI.Tests.Services
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> _roleRepository = new Mock<IRoleRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<RoleService>> _logger = new Mock<ILogger<RoleService>>();
        private readonly RoleService _sut;
        public RoleServiceTests()
        {
            _sut = new RoleService(_roleRepository.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsRoleDtos()
        {
            _roleRepository.Setup(r => r.GetAll())
                .ReturnsAsync(RoleSeeder.GetRoles());

            _mapper.Setup(m => m.Map<List<RoleDto>>(It.IsAny<List<Role>>()))
                .Returns(RoleSeeder.GetRoleDtos());

            var result = await _sut.GetAll();

            IsType<List<RoleDto>>(result);
            NotEmpty(result);
        }

        [Fact]
        public async Task GetById_ReturnsRole_IfExists()
        {
            _roleRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                .Returns(RoleSeeder.GetRoleDto());

            var role = RoleSeeder.GetRole();
            var result = await _sut.GetById(role.Id);

            IsType<RoleDto>(result);
            NotNull(result);
        }

        [Fact]
        public async Task GetById_ThrowsNotFoundException_IfRoleDoesNotExist()
        {
            _roleRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                .Returns(RoleSeeder.GetRoleDto());

            var role = RoleSeeder.GetRole();


            Func<Task> act = () => _sut.GetById(role.Id);
            await ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task Create_ReturnsCreatedRoleDto_IfCreated()
        {
            _roleRepository.Setup(r => r.Create(It.IsAny<Role>()))
                .ReturnsAsync(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<Role>(It.IsAny<CreateRoleDto>()))
                .Returns(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
               .Returns(RoleSeeder.GetRoleDto());

            var dto = new CreateRoleDto()
            {
                Name = "Created Name"
            };

            var result = await _sut.Create(dto);

            IsType<RoleDto>(result);

        }

        [Fact]
        public async Task Create_ThrowsAnyException_IfFailedToCreateRole()
        {


            _roleRepository.Setup(r => r.Create(It.IsAny<Role>()))
                    .ReturnsAsync(() => null);

            _mapper.Setup(m => m.Map<Role>(It.IsAny<CreateRoleDto>()))
                    .Returns(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                   .Returns(RoleSeeder.GetRoleDto());
            var dto = new CreateRoleDto()
            {
                Name = "Created Name"
            };

            Func<Task> act = () => _sut.Create(dto);

            await ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedUserDto_IfExists()
        {

            _roleRepository.Setup(r => r.Delete(It.IsAny<int>()))
                    .ReturnsAsync(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                   .Returns(RoleSeeder.GetRoleDto());

            var role = RoleSeeder.GetRole();
            var result = await _sut.Delete(role.Id);

            IsType<RoleDto>(result);
            NotNull(result);

        }

        [Fact]
        public async Task Delete_ThrowsNotFoundException_IfRoleDoesNotExist()
        {
            _roleRepository.Setup(r => r.Delete(It.IsAny<int>()))
                    .ReturnsAsync(() => null);

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                   .Returns(RoleSeeder.GetRoleDto());

            var role = RoleSeeder.GetRole();

            Func<Task> act = () => _sut.Delete(role.Id);

            await ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedRoleDto_IfExists()
        {
            var mockRole = RoleSeeder.GetRole();
            _roleRepository.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Role>()))
                    .ReturnsAsync(mockRole);

            _roleRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                   .Returns(RoleSeeder.GetRoleDto());
            

            var id = RoleSeeder.GetRole().Id;
            var dto = new CreateRoleDto()
            {
                Name = "UpdatedName"
            };
            

            var result = await _sut.Update(id, dto);

            IsType<RoleDto>(result);
            NotNull(result);
        }

        [Fact]
        public async Task Update_ThrowsNotFoundException_IfRoleDoesNotExist()
        {
            _roleRepository.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Role>()))
                    .ReturnsAsync(() => null);

            _roleRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync(RoleSeeder.GetRole());

            _mapper.Setup(m => m.Map<RoleDto>(It.IsAny<Role>()))
                   .Returns(RoleSeeder.GetRoleDto());
           
            _mapper.Setup(m => m.Map<Role>(It.IsAny<CreateRoleDto>()))
                   .Returns(RoleSeeder.GetRole());

            var id = RoleSeeder.GetRole().Id;
            var dto = new CreateRoleDto()
            {
                
                Name = "UpdatedName"
            };
            

           Func<Task> act = () => _sut.Update(id, dto);

           await ThrowsAsync<NotFoundException>(act);

        }
    }
}
