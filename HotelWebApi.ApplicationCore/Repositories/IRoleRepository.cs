using HotelWebApi.ApplicationCore.Entities.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> Create(Role role);
        Task<Role> Delete(int id);
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<Role> Update(int id, Role roleEntity);
    }
}