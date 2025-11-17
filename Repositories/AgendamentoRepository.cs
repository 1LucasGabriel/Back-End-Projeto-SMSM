using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class AgendamentoRepository: IAgendamentoRepository
    {
        private readonly MyDbContext _dbContext;

        public AgendamentoRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Agendamento> Post(Agendamento agendamento)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await _dbContext.Agendamentos.AddAsync(agendamento);

                var vaga = await _dbContext.Vagas.FindAsync(agendamento.IdVaga);
                if (vaga == null)
                    throw new Exception("Vaga não encontrada.");

                if (vaga.Quantidade <= 0)
                    throw new Exception("Não há vagas disponíveis.");

                vaga.Quantidade -= 1;
                _dbContext.Vagas.Update(vaga);

                var demanda = await _dbContext.Demandas.FindAsync(agendamento.IdDemanda);
                if (demanda == null)
                    throw new Exception("Demanda não encontrada.");

                demanda.Status = "Agendado";
                _dbContext.Demandas.Update(demanda);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return agendamento;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<bool> Delete(int id)
        {
            var agendamento = await GetById(id);
            if (agendamento == null)
                throw new Exception("Agendamento não encontrado");

            _dbContext.Agendamentos.Remove(agendamento);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Agendamento>> GetAll()
        {
            return await _dbContext.Agendamentos.ToListAsync();
        }

        public async Task<Agendamento> GetById(int id)
        {
            return await _dbContext.Agendamentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Agendamento> Put(Agendamento agendamento, int id)
        {
            var agendamentoPorId = await GetById(id);
            if (agendamentoPorId == null)
                throw new Exception("Agendamento não encontrado");

            agendamentoPorId.IdDemanda = agendamento.IdDemanda;
            agendamentoPorId.IdVaga = agendamento.IdVaga;
            agendamentoPorId.IdUsuarioRegulador = agendamento.IdUsuarioRegulador;
            agendamentoPorId.DataAgendamento = agendamento.DataAgendamento;
            agendamentoPorId.DataRealizacao = agendamento.DataRealizacao;
            agendamentoPorId.StatusComparecimento = agendamento.StatusComparecimento;

            _dbContext.Agendamentos.Update(agendamentoPorId);
            await _dbContext.SaveChangesAsync();
            return agendamentoPorId;
        }


        public async Task<List<dynamic>> GetAllComposto()
        {
            var agendamentos = await _dbContext.Agendamentos

                .Join(
                    _dbContext.Demandas,
                    a => a.IdDemanda,
                    d => d.Id,
                    (a, d) => new { Agendamento = a, Demanda = d }
                )

                .Join(
                    _dbContext.Pacientes,
                    ad => ad.Demanda.IdPaciente,
                    p => p.Id,
                    (ad, p) => new { ad.Agendamento, ad.Demanda, Paciente = p }
                )

                .Join(
                    _dbContext.Procedimentos,
                    adp => adp.Demanda.IdProcedimento,
                    pr => pr.Id,
                    (adp, pr) => new {
                        adp.Agendamento.Id,
                        adp.Agendamento.IdDemanda,
                        adp.Agendamento.IdVaga,
                        adp.Agendamento.IdUsuarioRegulador,
                        adp.Agendamento.DataAgendamento,
                        adp.Agendamento.DataRealizacao,
                        adp.Agendamento.StatusComparecimento,

                        PacienteNome = adp.Paciente.Nome,
                        ProcedimentoNome = pr.NomeProcedimento
                    }
                )

                .ToListAsync<dynamic>();

            return agendamentos;
        }

    }
}
