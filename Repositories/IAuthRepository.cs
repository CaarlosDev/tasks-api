using tasks_api.Models;

namespace tasks_api.Repositories
{
    public interface IAuthRepository
    {
        Task<Usuario?> GetUserByEmailAsync(string email);
        Task<Usuario?> GetUserByIdAsync(int userId);
        Task<Usuario> CreateUserAsync(Usuario user);
    }
}
