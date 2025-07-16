using Microsoft.VisualBasic;
using tasks_api.Models;

namespace tasks_api.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UsuarioResponseDTO User { get; set; }
    }
}
