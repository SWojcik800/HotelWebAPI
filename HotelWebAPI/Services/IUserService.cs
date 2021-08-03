using HotelWebAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public interface IUserService
    {
        Task<UserDto> Create(CreateUserDto dto);
        Task<UserDto> Delete(int id);
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Update(int id, UpdateUserDto dto);
    }
}