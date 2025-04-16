using GestoTF2.Models;

namespace GestorTF.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);

        Task<bool> GetUserByEmailAsync(string email);
    }
}