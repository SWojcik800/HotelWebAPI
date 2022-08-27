using HotelWebApi.ApplicationCore.Entities.ApiData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.Contracts.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> Create(Hotel hotelToCreate);
        Task<Hotel> Delete(int id);
        Task<List<Hotel>> GetAll();
        Task<Hotel> GetById(int id);
        Task<Hotel> Update(int id, Hotel hotelToUpdate);
    }
}