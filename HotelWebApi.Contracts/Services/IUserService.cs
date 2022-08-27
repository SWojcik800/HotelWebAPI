using HotelWebApi.Contracts.Dtos;
using HotelWebApi.Contracts.Dtos.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Services
{
    public interface IUserService
    {
        Task<int> Create(CreateUserDto dto);
        Task<UserDto> Delete(int id);
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Update(int id, UpdateUserDto dto);
    }
}