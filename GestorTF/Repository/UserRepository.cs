﻿using GestorTF.Models;
using GestorTF.Models.ViewModels.UserViewModel;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextApp _context;

        public UserRepository(ContextApp context)
        {
            _context = context;
        }

        //Metodos
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await AddUserRoleAsync(user);
            return user;
        }

        public async Task AddUserRoleAsync(User user)
        {
            // Encontrar o Role pelo nome
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (role != null)
            {
                // Verificar se o usuário já tem esse papel (Role)
                var userRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);

                if (userRole == null)
                {
                    // Adicionar a relação entre o usuário e o papel (Role)
                    _context.UserRoles.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}