using GestorTF.Models;
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

        public UserService(ContextApp context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserAsync()
        {
            var userList = await _context.Users.ToArrayAsync();
            return Ok(userList);
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