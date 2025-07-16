using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tasks_api.DTOs;
using tasks_api.Services;

namespace tasks_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TarefasController(TarefaService service) : AppController
    {
        private readonly TarefaService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? date)
        {
            var tarefas = await _service.ListAsync(GetUserId(), date);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarefaDTO dto)
        {
            var tarefa = await _service.CreateAsync(GetUserId(), dto);
            return CreatedAtAction(nameof(Get), new { date = tarefa.Date }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TarefaDTO dto)
        {
            var success = await _service.UpdateAsync(id, GetUserId(), dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id, GetUserId());
            return success ? NoContent() : NotFound();
        }
    }
}