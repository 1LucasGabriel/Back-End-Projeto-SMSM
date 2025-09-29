using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class UnidadeSaudeRepository : IUnidadeSaudeRepository
    {
        private readonly MyDbContext _dbContext;
        public UnidadeSaudeRepository(MyDbContext projetoDbContext)
        {
            _dbContext = projetoDbContext;
        }
        public async Task<bool> Delete(int id)
        {
            UnidadeSaude unidadePorId = await GetById(id);
            if (unidadePorId == null)
            {
                throw new Exception("Unidade não encontrada");
            }

            _dbContext.UnidadesSaude.Remove(unidadePorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<UnidadeSaude>> GetAll()
        {
            return await _dbContext.UnidadesSaude.ToListAsync();
        }

        public async Task<UnidadeSaude> GetById(int id)
        {
            return await _dbContext.UnidadesSaude.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UnidadeSaude> Post(UnidadeSaude unidadeSaude)
        {
            await _dbContext.UnidadesSaude.AddAsync(unidadeSaude);
            await _dbContext.SaveChangesAsync();

            return unidadeSaude;
        }

        public async Task<UnidadeSaude> Put(UnidadeSaude unidadeSaude, int id)
        {
            UnidadeSaude unidadePorId = await GetById(id);
            if (unidadePorId == null)
            {
                throw new Exception("Unidade não encontrada");
            }
            unidadePorId.Nome = unidadeSaude.Nome;
            unidadePorId.Tipo = unidadeSaude.Tipo;
            unidadePorId.Regiao = unidadeSaude.Regiao;

            _dbContext.UnidadesSaude.Update(unidadePorId);
            await _dbContext.SaveChangesAsync();

            return unidadePorId;
        }
    }
}
