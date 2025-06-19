using GestorTF.Models;
using GestoTF2.Data;

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
    }
}
