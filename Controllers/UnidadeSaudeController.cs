using APIProjeto.Models;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadeSaudeController: ControllerBase
    {
        private readonly IUnidadeSaudeRepository _unidadeSaudeRepository;
        public UnidadeSaudeController(IUnidadeSaudeRepository unidadeSaudeRepository)
        {
            _unidadeSaudeRepository = unidadeSaudeRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<UnidadeSaude>>> GetAll()
        {
            List<UnidadeSaude> unidadeSaudes = await _unidadeSaudeRepository.GetAll();
            return Ok(unidadeSaudes);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<UnidadeSaude>> GetById(int id)
        {
            UnidadeSaude unidade = await _unidadeSaudeRepository.GetById(id);
            return Ok(unidade);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<UnidadeSaude>> Post([FromBody] UnidadeSaude Unidade)
        {
            UnidadeSaude unidade = await _unidadeSaudeRepository.Post(Unidade);

            return Ok(unidade);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<UnidadeSaude>> Put([FromBody] UnidadeSaude Unidade, int id)
        {
            Unidade.Id = id;
            UnidadeSaude unidade = await _unidadeSaudeRepository.Put(Unidade, id);
            return Ok(unidade);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<UnidadeSaude>> Delete(int id)
        {
            bool apagado = await _unidadeSaudeRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
