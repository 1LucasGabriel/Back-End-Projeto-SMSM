using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeController(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult<List<Especialidade>>> GetAll()
        {
            var especialidades = await _especialidadeRepository.GetAll();
            return Ok(especialidades);
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<Especialidade>> GetById(int id)
        {
            var especialidade = await _especialidadeRepository.GetById(id);
            return Ok(especialidade);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Especialidade>> Post([FromBody] Especialidade especialidade)
        {
            var esp = await _especialidadeRepository.Post(especialidade);
            return Ok(esp);
        }

        [HttpPut("Atualizar{id}")]
        public async Task<ActionResult<Especialidade>> Put([FromBody] Especialidade especialidade, int id)
        {
            especialidade.Id = id;
            var esp = await _especialidadeRepository.Put(especialidade, id);
            return Ok(esp);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var apagado = await _especialidadeRepository.Delete(id);
            return Ok(apagado);
        }
    }
}
