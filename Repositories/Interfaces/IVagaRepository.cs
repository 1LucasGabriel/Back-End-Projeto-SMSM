using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IVagaRepository
    {
        Task<List<Vaga>> GetAll();
        Task<Vaga> GetById(int id);
        Task<Vaga> Post(Vaga vaga);
        Task<Vaga> Put(Vaga vaga, int id);
        Task<bool> Delete(int id);
    }
}
