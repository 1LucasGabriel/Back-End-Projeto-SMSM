using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IProcedimentoRepository
    {
        Task<List<Procedimento>> GetAll();
        Task<Procedimento> GetById(int id);
        Task<Procedimento> Post(Procedimento procedimento);
        Task<Procedimento> Put(Procedimento procedimento, int id);
        Task<bool> Delete(int id);
    }
}
