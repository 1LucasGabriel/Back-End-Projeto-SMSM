using APIProjeto.Models.DTOs;
using APIProjeto.Repositories.Interfaces;
using APIProjeto.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;


namespace APIProjeto.Controllers
{
    public class AuthController: ControllerBase
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUsuarioRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetByCpfAsync(loginDto.Cpf);
            if (user == null || user.Senha != loginDto.Senha)
            {
                return Unauthorized(new { message = "CPF ou senha incorretos" });
            }

            var token = _tokenService.GenerateToken(user); // JWT
            return Ok(new { token, userId = user.Id, nome = user.Nome, perfil = user.IdPerfil });
        }
    }
}
