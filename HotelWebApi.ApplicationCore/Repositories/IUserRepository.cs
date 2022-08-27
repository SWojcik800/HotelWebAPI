using HotelWebApi.ApplicationCore.Entities.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> Delete(int id);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Update(int id, User updatedUser);
    }
}