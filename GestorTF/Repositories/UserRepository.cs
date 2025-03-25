using GestorTF.Models;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorTF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextApp _context;

        public UserRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(x => x.Role)
                .AsNoTracking()
                .Select(user => new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Role = new Role { Name = user.Role.Name }
                })
                .ToListAsync();

            return users;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
       
        
    }

}
