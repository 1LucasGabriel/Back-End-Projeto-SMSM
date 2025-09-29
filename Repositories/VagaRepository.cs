using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class VagaRepository: IVagaRepository
    {
        private readonly MyDbContext _dbContext;

        public VagaRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vaga> Post(Vaga vaga)
        {
            await _dbContext.Vagas.AddAsync(vaga);
            await _dbContext.SaveChangesAsync();
            return vaga;
        }

        public async Task<bool> Delete(int id)
        {
            var vaga = await GetById(id);
            if (vaga == null)
                throw new Exception("Vaga não encontrada");

            _dbContext.Vagas.Remove(vaga);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Vaga>> GetAll()
        {
            return await _dbContext.Vagas.ToListAsync();
        }

        public async Task<Vaga> GetById(int id)
        {
            return await _dbContext.Vagas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Vaga> Put(Vaga vaga, int id)
        {
            var vagaPorId = await GetById(id);
            if (vagaPorId == null)
                throw new Exception("Vaga não encontrada");

            vagaPorId.IdProcedimento = vaga.IdProcedimento;
            vagaPorId.IdUnidadeOfertante = vaga.IdUnidadeOfertante;
            vagaPorId.Quantidade = vaga.Quantidade;
            vagaPorId.MesAnoReferencia = vaga.MesAnoReferencia;

            _dbContext.Vagas.Update(vagaPorId);
            await _dbContext.SaveChangesAsync();
            return vagaPorId;
        }
    }
}
