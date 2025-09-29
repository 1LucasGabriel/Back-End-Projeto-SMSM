using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController: ControllerBase
    {
        private readonly IPerfilRepository _perfilRepository;
        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Perfil>>> GetAll()
        {
            List<Perfil> perfis = await _perfilRepository.GetAll();
            return Ok(perfis);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Perfil>> GetById(int id)
        {
            Perfil perfil = await _perfilRepository.GetById(id);
            return Ok(perfil);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Perfil>> Post([FromBody] Perfil Perfil)
        {
            Perfil perfil = await _perfilRepository.Post(Perfil);

            return Ok(perfil);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Perfil>> Put([FromBody] Perfil Perfil, int id)
        {
            Perfil.Id = id;
            Perfil perfil = await _perfilRepository.Put(Perfil, id);
            return Ok(perfil);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<Perfil>> Delete(int id)
        {
            bool apagado = await _perfilRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
