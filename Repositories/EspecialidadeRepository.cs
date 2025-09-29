using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class EspecialidadeRepository: IEspecialidadeRepository
    {
        private readonly MyDbContext _dbContext;

        public EspecialidadeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Especialidade> Post(Especialidade especialidade)
        {
            await _dbContext.Especialidades.AddAsync(especialidade);
            await _dbContext.SaveChangesAsync();
            return especialidade;
        }

        public async Task<bool> Delete(int id)
        {
            var especialidade = await GetById(id);
            if (especialidade == null)
                throw new Exception("Especialidade não encontrada");

            _dbContext.Especialidades.Remove(especialidade);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Especialidade>> GetAll()
        {
            return await _dbContext.Especialidades.ToListAsync();
        }

        public async Task<Especialidade> GetById(int id)
        {
            return await _dbContext.Especialidades.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Especialidade> Put(Especialidade especialidade, int id)
        {
            var especialidadePorId = await GetById(id);
            if (especialidadePorId == null)
                throw new Exception("Especialidade não encontrada");

            especialidadePorId.Nome = especialidade.Nome;

            _dbContext.Especialidades.Update(especialidadePorId);
            await _dbContext.SaveChangesAsync();
            return especialidadePorId;
        }
    }
}
