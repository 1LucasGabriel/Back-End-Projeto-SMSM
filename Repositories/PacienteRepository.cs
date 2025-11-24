using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly MyDbContext _dbContext;
        public PacienteRepository(MyDbContext projetoDbContext)
        {
            _dbContext = projetoDbContext;
        }
        public async Task<bool> Delete(int id)
        {
            Paciente pacientePorId = await GetById(id);
            if (pacientePorId == null)
            {
                throw new Exception("Paciente não encontrado");
            }

            _dbContext.Pacientes.Remove(pacientePorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Paciente>> GetAll()
        {
            return await _dbContext.Pacientes.ToListAsync();
        }

        public async Task<Paciente> GetById(int id)
        {
            return await _dbContext.Pacientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Paciente> Post(Paciente paciente)
        {
            await _dbContext.Pacientes.AddAsync(paciente);
            await _dbContext.SaveChangesAsync();

            return paciente;
        }
        public async Task<Paciente> GetByCpfAsync(string cpf)
        {
            return await _dbContext.Pacientes.FirstOrDefaultAsync(x => x.Cpf == cpf);
        }

        public async Task<Paciente> Put(Paciente paciente, int id)
        {
            Paciente pacientePorId = await GetById(id);
            if (pacientePorId == null)
            {
                throw new Exception("Paciente não encontrado");
            }
            pacientePorId.Nome = paciente.Nome;
            pacientePorId.Cpf = paciente.Cpf;
            pacientePorId.Cns = paciente.Cns;
            pacientePorId.DataNascimento = paciente.DataNascimento;
            pacientePorId.Endereco = paciente.Endereco;

            _dbContext.Pacientes.Update(pacientePorId);
            await _dbContext.SaveChangesAsync();

            return pacientePorId;
        }
    }
}
