using Microsoft.EntityFrameworkCore;
using tasks_api.Data;
using tasks_api.Models;

namespace tasks_api.Repositories
{
    public class AuthRepository(AppDbContext context) : IAuthRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Usuario?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Usuario> CreateUserAsync(Usuario user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
