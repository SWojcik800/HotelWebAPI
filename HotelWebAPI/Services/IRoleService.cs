using HotelWebAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
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