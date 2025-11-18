using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandaController : ControllerBase
    {
        private readonly IDemandaRepository _demandaRepository;
        private readonly MyDbContext _dbContext;

        public DemandaController(IDemandaRepository demandaRepository, MyDbContext dbContext)
        {
            _demandaRepository = demandaRepository;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<ActionResult> GetAll()
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

        [HttpGet("demanda-especialidade")]
        public async Task<IActionResult> GetDemandasPorEspecialidade()
        {
            var resultado = await _dbContext.Demandas
                .Join(_dbContext.Procedimentos,
                    d => d.IdProcedimento,
                    p => p.Id,
                    (d, p) => new { d, p })
                .Join(_dbContext.Especialidades,
                    dp => dp.p.IdEspecialidade,
                    e => e.Id,
                    (dp, e) => new { EspecialidadeNome = e.Nome })
                .GroupBy(x => x.EspecialidadeNome)
                .Select(g => new
                {
                    label = g.Key,
                    value = g.Count()
                })
                .OrderByDescending(x => x.value)
                .ToListAsync();

            return Ok(resultado);
        }


    }
}
