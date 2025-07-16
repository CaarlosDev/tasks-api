using tasks_api.Models;

namespace tasks_api.Repositories
{
    public interface ITarefaRepository
    {
        Task<List<Tarefa>> GetTarefasByDateAsync(int userId, DateTime date);
        Task<List<Tarefa>> GetAllAsync(int userId);
        Task<Tarefa> GetByIdAsync(int id);
        Task<Tarefa> CreateAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(Tarefa tarefa);
    }
}
