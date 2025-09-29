using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private readonly MyDbContext _dbContext;
        public ProcedimentoRepository(MyDbContext projetoDbContext)
        {
            _dbContext = projetoDbContext;
        }
        public async Task<bool> Delete(int id)
        {
            Procedimento procedimentoPorId = await GetById(id);
            if (procedimentoPorId == null)
            {
                throw new Exception("Procedimento não encontrado");
            }

            _dbContext.Procedimentos.Remove(procedimentoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Procedimento>> GetAll()
        {
            return await _dbContext.Procedimentos.ToListAsync();
        }

        public async Task<Procedimento> GetById(int id)
        {
            return await _dbContext.Procedimentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Procedimento> Post(Procedimento procedimento)
        {
            await _dbContext.Procedimentos.AddAsync(procedimento);
            await _dbContext.SaveChangesAsync();

            return procedimento;
        }

        public async Task<Procedimento> Put(Procedimento procedimento, int id)
        {
            Procedimento procedimentoPorId = await GetById(id);
            if (procedimentoPorId == null)
            {
                throw new Exception("Procedimento não encontrado");
            }
            procedimentoPorId.CodigoProcedimento = procedimento.CodigoProcedimento;
            procedimentoPorId.NomeProcedimento = procedimento.NomeProcedimento;
            procedimentoPorId.Tipo = procedimento.Tipo;
            procedimentoPorId.IdEspecialidade = procedimento.IdEspecialidade;

            _dbContext.Procedimentos.Update(procedimentoPorId);
            await _dbContext.SaveChangesAsync();

            return procedimentoPorId;
        }
    }
}
