using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tasks_api.DTOs;
using tasks_api.Services;

namespace tasks_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : AppController
    {
        private readonly AuthService _authService = authService;

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserByToken()
        {
            var userId = GetUserId();
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user == null)
                return BadRequest("E-mail já cadastrado.");

            return Ok(new { user.Id, user.Name, user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response == null)
                return Unauthorized("Credenciais inválidas.");

            return Ok(response);
        }
    }
}
