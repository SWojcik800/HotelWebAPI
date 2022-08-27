using HotelWebApi.ApplicationCore.Contexts;
using HotelWebApi.ApplicationCore.Entities.Authentication;
using HotelWebApi.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelWebApi.ApplicationCore.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HotelWebAPIDbContext _context;

        public RoleRepository(HotelWebAPIDbContext context)
        {
            _context = context;

        }

        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> Create(Role role)
        {
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> Delete(int id)
        {
            var roleToRemove = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (roleToRemove is null)
                return null;

            _context.Roles.Remove(roleToRemove);
            await _context.SaveChangesAsync();
            return roleToRemove;
        }

        public async Task<Role> Update(int id, Role roleEntity)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role is null || roleEntity.Id != id)
                return null;

            _context.Roles.Update(roleEntity);
            await _context.SaveChangesAsync();

            return roleEntity;
        }
    }
}
