using HotelWebApi.Contracts.Dtos.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Services
{
    public interface IRoleService
    {
        Task<RoleDto> Create(CreateRoleDto dto);
        Task<RoleDto> Delete(int id);
        Task<List<RoleDto>> GetAll();
        Task<RoleDto> GetById(int id);
        Task<RoleDto> Update(int id, CreateRoleDto dto);
    }
}