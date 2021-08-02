using HotelWebAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebAPI.Services
{
    public interface IHotelService
    {
        Task<int?> Create(CreateHotelDto dto);
        Task<HotelDto> Delete(int id);
        Task<List<HotelDto>> GetAll();
        Task<HotelDto> GetById(int id);
        Task<HotelDto> Update(int id, UpdateHotelDto dto);
    }
}