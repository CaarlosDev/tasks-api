using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using tasks_api.DTOs;
using tasks_api.Models;
using tasks_api.Repositories;

namespace tasks_api.Services
{
    public class AuthService(IAuthRepository authRepository, IConfiguration config)
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IConfiguration _config = config;

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PwdHash, user.PwdSalt))
                return null;

            string token = CreateToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                User = new UsuarioResponseDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                }
            };
        }

        public async Task<UsuarioResponseDTO?> RegisterAsync(RegisterDTO dto)
        {
            if (await _authRepository.GetUserByEmailAsync(dto.Email) != null)
                return null;

            CreatePwdHash(dto.Password, out byte[] hash, out byte[] salt);

            var user = new Usuario
            {
                Name = dto.Name,
                Email = dto.Email,
                PwdHash = hash,
                PwdSalt = salt
            };

            var createdUser = await _authRepository.CreateUserAsync(user);

            return new UsuarioResponseDTO
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
            };
        }

        public async Task<UsuarioResponseDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _authRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UsuarioResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        private void CreatePwdHash(string pwd, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd));
        }

        private bool VerifyPassword(string pwd, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var hashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            return hashComputado.SequenceEqual(hash);
        }

        private string CreateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
