using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using SREO.Application.DTOs;
using SREO.Application.Interfaces;
using SREO.Domain.Entities;
using SREO.Domain.Interfaces;

namespace SREO.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _jwtSecret = "mi_clave_secreta_para_jwt_muy_larga_y_segura";

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var usuarioExistente = await _usuarioRepository.GetByCorreoAsync(registerDto.CorreoElectronico);
            if (usuarioExistente != null)
            {
                throw new Exception("El correo electrónico ya está registrado.");
            }

            if (!Enum.TryParse<RolUsuario>(registerDto.Rol, out var rol))
            {
                throw new Exception("Rol inválido.");
            }

            var contrasenaHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Contrasena);

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = registerDto.Nombre,
                CorreoElectronico = registerDto.CorreoElectronico,
                ContrasenaHash = contrasenaHash,
                Rol = rol
            };

            await _usuarioRepository.AddAsync(usuario);

            var token = GenerarToken(usuario);

            return new AuthResponseDto
            {
                Token = token,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol.ToString()
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(loginDto.CorreoElectronico);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Contrasena, usuario.ContrasenaHash))
            {
                throw new Exception("Credenciales inválidas.");
            }

            var token = GenerarToken(usuario);

            return new AuthResponseDto
            {
                Token = token,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol.ToString()
            };
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "SREO",
                audience: "SREO",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}