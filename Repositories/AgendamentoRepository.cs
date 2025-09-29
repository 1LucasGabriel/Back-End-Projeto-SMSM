using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
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
            await _dbContext.Agendamentos.AddAsync(agendamento);
            await _dbContext.SaveChangesAsync();
            return agendamento;
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
    }
}
