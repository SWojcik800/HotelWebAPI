using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelWebAPIDbContext _context;
        public HotelRepository(HotelWebAPIDbContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAll()
        {
            return await _context
                .Hotels
                .Include(h => h.Address)
                .ToListAsync();

        }

        public async Task<Hotel> GetById(int id)
        {
            return await _context
                .Hotels
                .Include(h => h.Address)
                .FirstOrDefaultAsync(h => h.Id == id);

        }

        public async Task<Hotel> Create(Hotel hotelToCreate)
        {
            await _context.AddAsync(hotelToCreate);
            await _context.SaveChangesAsync();
            return hotelToCreate;
        }

        public async Task<Hotel> Update(int id, Hotel hotelToUpdate)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (id != hotelToUpdate.Id || hotel == null)
                return null;

            _context.Hotels.Update(hotelToUpdate);
            await _context.SaveChangesAsync();

            return hotelToUpdate;
        }

        public async Task<Hotel> Delete(int id)
        {
            var hotelToDelete = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (hotelToDelete is null)
                return null;

            _context.Hotels.Remove(hotelToDelete);
            await _context.SaveChangesAsync();

            return hotelToDelete;
        }
    }
}
