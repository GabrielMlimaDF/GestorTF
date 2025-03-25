using GestorTF.Models;
using GestoTF2.Data;
using GestoTF2.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.Services
{
    public class UserService
    {
        private readonly ContextApp _context;

        //precisa mudar este Metodo para IREPOSITORIES  
        public UserService(ContextApp context)
        {
            _context = context;
        }
        public async Task AddUserRoleAsync(User user, string roleName)
        {
            // Encontrar o Role pelo nome
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

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
