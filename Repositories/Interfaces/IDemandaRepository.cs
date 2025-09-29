using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IDemandaRepository
    {
        Task<List<Demanda>> GetAll();
        Task<Demanda> GetById(int id);
        Task<Demanda> Post(Demanda demanda);
        Task<Demanda> Put(Demanda demanda, int id);
        Task<bool> Delete(int id);
    }
}
