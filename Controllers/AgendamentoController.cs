using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly MyDbContext _dbContext;

        public AgendamentoController(IAgendamentoRepository agendamentoRepository, MyDbContext dbContext)
        {
            _agendamentoRepository = agendamentoRepository;
            _dbContext = dbContext;
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

        [HttpGet]
        [Route("BuscarComposto")]
        public async Task<ActionResult> GetAllComposto()
        {
            var agendamentos = await _agendamentoRepository.GetAllComposto();
            return Ok(agendamentos);
        }

        [HttpGet("fila/posicao/{idPaciente}")]
        public async Task<IActionResult> GetPosicaoNaFilaAsync(int idPaciente)
        {
            var demandasPaciente = await (
                from d in _dbContext.Demandas
                join proc in _dbContext.Procedimentos on d.IdProcedimento equals proc.Id
                join med in _dbContext.Usuarios on d.IdUsuarioSolicitante equals med.Id
                where d.IdPaciente == idPaciente && d.Status == "Pendente"
                select new
                {
                    d.Id,
                    d.IdProcedimento,
                    d.DataSolicitacao,
                    d.Justificativa,
                    ProcedimentoNome = proc.NomeProcedimento,
                    MedicoNome = med.Nome
                }
            ).ToListAsync();

            if (!demandasPaciente.Any())
                return NotFound(new { message = "O paciente não possui demandas pendentes." });

            var resultado = new List<object>();

            foreach (var demanda in demandasPaciente)
            {
                var fila = await (
                    from d in _dbContext.Demandas
                    join p in _dbContext.Pacientes on d.IdPaciente equals p.Id
                    join proc in _dbContext.Procedimentos on d.IdProcedimento equals proc.Id
                    where d.IdProcedimento == demanda.IdProcedimento
                          && d.Status == "Pendente"
                    orderby d.DataSolicitacao ascending
                    select new
                    {
                        d.IdPaciente,
                        PacienteNome = p.Nome,
                        ProcedimentoNome = proc.NomeProcedimento,
                        d.DataSolicitacao
                    }
                ).ToListAsync();

                var posicao = fila.FindIndex(x => x.IdPaciente == idPaciente) + 1;

                var item = fila.First(x => x.IdPaciente == idPaciente);

                resultado.Add(new
                {
                    IdPaciente = idPaciente,
                    item.PacienteNome,
                    item.ProcedimentoNome,
                    demanda.Justificativa,
                    demanda.MedicoNome,
                    Posicao = posicao,
                    TotalNaFila = fila.Count
                });
            }

            return Ok(resultado);
        }

    }
}
