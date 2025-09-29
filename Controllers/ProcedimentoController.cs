using APIProjeto.Models;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedimentoController: ControllerBase
    {
        private readonly IProcedimentoRepository _procedimentoRepository;
        public ProcedimentoController(IProcedimentoRepository procedimentoRepository)
        {
            _procedimentoRepository = procedimentoRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Procedimento>>> GetAll()
        {
            List<Procedimento> procedimentos = await _procedimentoRepository.GetAll();
            return Ok(procedimentos);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Procedimento>> GetById(int id)
        {
            Procedimento procedimento = await _procedimentoRepository.GetById(id);
            return Ok(procedimento);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Procedimento>> Post([FromBody] Procedimento Procedimento)
        {
            Procedimento procedimento = await _procedimentoRepository.Post(Procedimento);

            return Ok(procedimento);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Procedimento>> Put([FromBody] Procedimento Procedimento, int id)
        {
            Procedimento.Id = id;
            Procedimento procedimento = await _procedimentoRepository.Put(Procedimento, id);
            return Ok(procedimento);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<Procedimento>> Delete(int id)
        {
            bool apagado = await _procedimentoRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
