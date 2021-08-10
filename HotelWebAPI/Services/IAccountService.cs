using HotelWebAPI.Models.Dtos;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public interface IAccountService
    {
        Task<int> Register(RegisterUserDto dto);
        Task<string> GenerateJwtToken(LoginDto dto);
    }
}