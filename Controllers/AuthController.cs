using APIProjeto.Models.DTOs;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using APIProjeto.Services.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;


namespace APIProjeto.Controllers
{
    public class AuthController: ControllerBase
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUsuarioRepository userRepository, ITokenService tokenService, IPacienteRepository pacienteRepository)
        {
            _userRepository = userRepository;
            _pacienteRepository = pacienteRepository;
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

        [HttpPost("login/paciente")]
        public async Task<IActionResult> LoginPaciente([FromBody] LoginDto loginDto)
        {
            var paciente = await _pacienteRepository.GetByCpfAsync(loginDto.Cpf);

            if (paciente == null)
                return Unauthorized(new { message = "CPF não encontrado" });

            if (paciente.Cns != loginDto.Senha)
                return Unauthorized(new { message = "Senha incorreta (use seu CNS)" });

            var token = _tokenService.GenerateTokenPaciente(paciente);

            return Ok(new
            {
                token,
                userId = paciente.Id,
                nome = paciente.Nome,
                perfil = 3
            });
        }

    }
}
