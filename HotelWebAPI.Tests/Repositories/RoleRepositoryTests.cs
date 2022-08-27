using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.ApplicationCore.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests.Repositories
{
    public class RoleRepositoryTests : RoleAPIDbTest
    {
        private readonly RoleRepository _sut;

        public RoleRepositoryTests()
        {
            _sut = new RoleRepository(context);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfRoles()
        {
            var result = await _sut.GetAll();

            IsType<List<Role>>(result);
            NotEmpty(result);
        }

        [Fact]
        public async Task GetById_ReturnsRole()
        {
            var role = context.Roles.First();
            var result = await _sut.GetById(role.Id);

            IsType<Role>(result);
            NotNull(result);
        }
        [Fact]
        public async Task GetById_ReturnsNull_IfRoleDoesNotExist()
        {
            var role = context.Roles.First();
            var result = await _sut.GetById(role.Id - 1);
            Null(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedRole()
        {
            var newRole = new Role()
            {
                Name = "RandomRoleName"
            };

            var result = await _sut.Create(newRole);

            IsType<Role>(result);
            NotNull(result);
            NotNull(context.Roles.FirstOrDefault(r => r.Name == newRole.Name));
        }

        [Fact]
        public async Task Delete_ReturnsDeletedRole()
        {
            var role = context.Roles.First();
            var result = await _sut.Delete(role.Id);

            IsType<Role>(result);
            Null(context.Roles.FirstOrDefault(r => r.Id == role.Id));
        }

        [Fact]
        public async Task Delete_ReturnsNull_IfRoleDoesNotExist()
        {
            var role = context.Roles.First();
            var result = await _sut.Delete(role.Id - 1);

            Null(result);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedRole_IfExists()
        {
            var role = context.Roles.First();
            role.Name = "UpdatedName";

            

            var result = await _sut.Update(role.Id, role);
            IsType<Role>(result);
        }

        [Fact]
        public async Task Update_ReturnsNull_IfRoleDoesNotExist()
        {
            var role = context.Roles.First();
            role.Name = "UpdatedName";



            var result = await _sut.Update(role.Id - 1 , role);
            Null(result);
        }
        


    }
}
