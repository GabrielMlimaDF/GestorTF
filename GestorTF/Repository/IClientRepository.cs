using GestorTF.Models;

namespace GestorTF.Repository
{
    public interface IClientRepository
    {
        Task<Cliente> CreateClientAsync(Cliente cliente);
    }
}
