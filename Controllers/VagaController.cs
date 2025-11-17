using APIProjeto.Models;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VagaController : ControllerBase
    {
        private readonly IVagaRepository _vagaRepository;

        public VagaController(IVagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Vaga>>> GetAll()
        {
            var vagas = await _vagaRepository.GetAll();
            return Ok(vagas);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Vaga>> GetById(int id)
        {
            var vaga = await _vagaRepository.GetById(id);
            return Ok(vaga);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Vaga>> Post([FromBody] Vaga vaga)
        {
            var v = await _vagaRepository.Post(vaga);
            return Ok(v);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Vaga>> Put([FromBody] Vaga vaga, int id)
        {
            vaga.Id = id;
            var v = await _vagaRepository.Put(vaga, id);
            return Ok(v);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var apagado = await _vagaRepository.Delete(id);
            return Ok(apagado);
        }

        [HttpGet("Unidades")]
        public async Task<ActionResult> GetVagasUnidades()
        {
            var resultado = await _vagaRepository.GetVagasComUnidades();
            return Ok(resultado);
        }

    }
}
