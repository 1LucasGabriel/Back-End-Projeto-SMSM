using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Task<List<Especialidade>> GetAll();
        Task<Especialidade> GetById(int id);
        Task<Especialidade> Post(Especialidade especialidade);
        Task<Especialidade> Put(Especialidade especialidade, int id);
        Task<bool> Delete(int id);
    }
}
