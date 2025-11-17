using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class DemandaRepository: IDemandaRepository
    {
        private readonly MyDbContext _dbContext;

        public DemandaRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Demanda> Post(Demanda demanda)
        {
            await _dbContext.Demandas.AddAsync(demanda);
            await _dbContext.SaveChangesAsync();
            return demanda;
        }

        public async Task<bool> Delete(int id)
        {
            Demanda demanda = await GetById(id);
            if (demanda == null)
                throw new Exception("Demanda não encontrada");

            _dbContext.Demandas.Remove(demanda);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<dynamic>> GetAll()
        {
            var demandas = await _dbContext.Demandas
                .Join(
                    _dbContext.Pacientes,
                    d => d.IdPaciente,
                    p => p.Id,
                    (d, p) => new { Demanda = d, Paciente = p }
                )
                .Join(
                    _dbContext.Procedimentos,
                    dp => dp.Demanda.IdProcedimento,
                    pr => pr.Id,
                    (dp, pr) => new {
                        dp.Demanda.Id,
                        dp.Demanda.IdPaciente,
                        PacienteNome = dp.Paciente.Nome,
                        dp.Demanda.DataSolicitacao,
                        dp.Demanda.Status,
                        dp.Demanda.Prioridade,
                        dp.Demanda.IdUnidadeSolicitante,
                        dp.Demanda.IdProcedimento,
                        ProcedimentoNome = pr.NomeProcedimento
                    }
                )
                .ToListAsync<dynamic>();

            return demandas;
        }




        public async Task<Demanda> GetById(int id)
        {
            return await _dbContext.Demandas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Demanda> Put(Demanda demanda, int id)
        {
            var demandaPorId = await GetById(id);
            if (demandaPorId == null)
                throw new Exception("Demanda não encontrada");

            demandaPorId.IdPaciente = demanda.IdPaciente;
            demandaPorId.IdProcedimento = demanda.IdProcedimento;
            demandaPorId.IdUnidadeSolicitante = demanda.IdUnidadeSolicitante;
            demandaPorId.IdUsuarioSolicitante = demanda.IdUsuarioSolicitante;
            demandaPorId.DataSolicitacao = demanda.DataSolicitacao;
            demandaPorId.Prioridade = demanda.Prioridade;
            demandaPorId.Status = demanda.Status;
            demandaPorId.Justificativa = demanda.Justificativa;

            _dbContext.Demandas.Update(demandaPorId);
            await _dbContext.SaveChangesAsync();
            return demandaPorId;
        }
    }
}
