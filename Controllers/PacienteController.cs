using APIProjeto.Models;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController: ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Paciente>>> GetAll()
        {
            List<Paciente> pacientes = await _pacienteRepository.GetAll();
            return Ok(pacientes);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Paciente>> GetById(int id)
        {
            Paciente paciente = await _pacienteRepository.GetById(id);
            return Ok(paciente);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Paciente>> Post([FromBody] Paciente Paciente)
        {
            Paciente paciente = await _pacienteRepository.Post(Paciente);

            return Ok(paciente);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Paciente>> Put([FromBody] Paciente Paciente, int id)
        {
            Paciente.Id = id;
            Paciente paciente = await _pacienteRepository.Put(Paciente, id);
            return Ok(paciente);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<Paciente>> Delete(int id)
        {
            bool apagado = await _pacienteRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
