using Microsoft.EntityFrameworkCore;
using tasks_api.Data;
using tasks_api.Models;

namespace tasks_api.Repositories
{
    public class TarefaRepository(AppDbContext context) : ITarefaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Tarefa>> GetTarefasByDateAsync(int userId, DateTime date)
        {
            return await _context.Tarefas
                .Where(t => t.UserId == userId && t.Date.Date == date.ToUniversalTime().Date)
                .ToListAsync();
        }

        public async Task<List<Tarefa>> GetAllAsync(int userId)
        {
            return await _context.Tarefas
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> CreateAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
