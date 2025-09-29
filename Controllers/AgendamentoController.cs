using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoController(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Agendamento>>> GetAll()
        {
            var agendamentos = await _agendamentoRepository.GetAll();
            return Ok(agendamentos);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Agendamento>> GetById(int id)
        {
            var agendamento = await _agendamentoRepository.GetById(id);
            return Ok(agendamento);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Agendamento>> Post([FromBody] Agendamento agendamento)
        {
            var a = await _agendamentoRepository.Post(agendamento);
            return Ok(a);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Agendamento>> Put([FromBody] Agendamento agendamento, int id)
        {
            agendamento.Id = id;
            var a = await _agendamentoRepository.Put(agendamento, id);
            return Ok(a);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var apagado = await _agendamentoRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
