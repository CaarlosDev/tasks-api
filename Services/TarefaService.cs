using tasks_api.DTOs;
using tasks_api.Models;
using tasks_api.Repositories;

namespace tasks_api.Services
{
    public class TarefaService(ITarefaRepository repo)
    {
        private readonly ITarefaRepository _repo = repo;

        public async Task<List<Tarefa>> ListAsync(int userId, DateTime? date)
        {
            if (date.HasValue)
            {
                return await _repo.GetTarefasByDateAsync(userId, date.Value.Date);
            }
            else
            {
                return await _repo.GetAllAsync(userId);
            }
        }

        public async Task<Tarefa> CreateAsync(int userId, TarefaDTO dto)
        {
            var tarefa = new Tarefa
            {
                Title = dto.Title,
                Date = dto.Date.ToUniversalTime(),
                Concluded = false,
                UserId = userId
            };

            return await _repo.CreateAsync(tarefa);
        }

        public async Task<bool> UpdateAsync(int id, int userId, TarefaDTO dto)
        {
            var tarefa = await _repo.GetByIdAsync(id);
            if (tarefa == null || tarefa.UserId != userId) return false;

            tarefa.Title = dto.Title;
            tarefa.Date = dto.Date.ToUniversalTime();

            await _repo.UpdateAsync(tarefa);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var tarefa = await _repo.GetByIdAsync(id);
            if (tarefa == null || tarefa.UserId != userId) return false;

            await _repo.DeleteAsync(tarefa);
            return true;
        }
    }
}
