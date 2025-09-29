using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly MyDbContext _dbContext;
        public PerfilRepository(MyDbContext projetoDbContext)
        {
            _dbContext = projetoDbContext;
        }
        public async Task<bool> Delete(int id)
        {
            Perfil perfilPorId = await GetById(id);
            if (perfilPorId == null)
            {
                throw new Exception("Perfil não encontrado");
            }

            _dbContext.Perfil.Remove(perfilPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Perfil>> GetAll()
        {
            return await _dbContext.Perfil.ToListAsync();
        }

        public async Task<Perfil> GetById(int id)
        {
            return await _dbContext.Perfil.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Perfil> Post(Perfil perfil)
        {
            await _dbContext.Perfil.AddAsync(perfil);
            await _dbContext.SaveChangesAsync();

            return perfil;
        }

        public async Task<Perfil> Put(Perfil perfil, int id)
        {
            Perfil perfilPorId = await GetById(id);
            if (perfilPorId == null)
            {
                throw new Exception("Perfil não encontrado");
            }
            perfilPorId.Nome = perfil.Nome;

            _dbContext.Perfil.Update(perfilPorId);
            await _dbContext.SaveChangesAsync();

            return perfilPorId;
        }
    }
}
