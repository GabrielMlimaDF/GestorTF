using GestorTF.Models;
using GestoTF2.Data;
using Microsoft.EntityFrameworkCore;

namespace GestorTF.Repository
{
    public class ClientRepository(ContextApp _contextApp) : IClientRepository
    {
        public async Task<Cliente> CreateClientAsync(Cliente cliente)
        {
            _contextApp.Clientes.Add(cliente);
            await _contextApp.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _contextApp.Clientes
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);
        }

        public async Task UpdateAsync(Cliente client)
        {
            _contextApp.Clientes.Update(client);
            await _contextApp.SaveChangesAsync();
        }
    }
}
