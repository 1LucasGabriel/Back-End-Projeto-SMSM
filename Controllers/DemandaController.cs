using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandaController : ControllerBase
    {
        private readonly IDemandaRepository _demandaRepository;

        public DemandaController(IDemandaRepository demandaRepository)
        {
            _demandaRepository = demandaRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Demanda>>> GetAll()
        {
            var demandas = await _demandaRepository.GetAll();
            return Ok(demandas);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Demanda>> GetById(int id)
        {
            var demanda = await _demandaRepository.GetById(id);
            return Ok(demanda);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Demanda>> Post([FromBody] Demanda demanda)
        {
            var d = await _demandaRepository.Post(demanda);
            return Ok(d);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Demanda>> Put([FromBody] Demanda demanda, int id)
        {
            demanda.Id = id;
            var d = await _demandaRepository.Put(demanda, id);
            return Ok(d);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var apagado = await _demandaRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
