using GestorTF.Models;
using GestorTF.ServicesSecurity;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.Services
{
    public class UserService : ControllerBase
    {
        private readonly ContextApp _context;
        private readonly AuthService _authService;

        public UserService(ContextApp context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        public async Task<List<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<(bool Success, string Message)> RegisterUserAsync(UserRegisterDto model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return (false, "Email já está em uso.");

            var (hashedPassword, salt) = _authService.HashPassword(model.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword,
                Salt = salt,
                IsActive = true,
                CreateAt = DateTime.UtcNow,
                RoleId = model.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await AddUserRoleAsync(user);

            return (true, "Usuário registrado com sucesso!");
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
    }
}