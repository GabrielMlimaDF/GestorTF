using GestorTF.Models;

namespace GestorTF.Repository
{
    public interface IClientRepository
    {
        Task<Cliente?> CreateClientAsync(Cliente client);
        Task<Cliente?> GetByIdAsync(Guid id, Guid userId);
        Task UpdateAsync(Cliente client);
        Task<bool> GetClientByCnpjAsync(string cnpj);
        Task<IEnumerable<Cliente?>> GetClientAllAsync(Guid userId);
    }
}
