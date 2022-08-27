using HotelWebApi.Contracts.Dtos.Authorization;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Services
{
    public interface IAccountService
    {
        Task<int> Register(RegisterUserDto dto);
        Task<string> GenerateJwtToken(LoginDto dto);
    }
}