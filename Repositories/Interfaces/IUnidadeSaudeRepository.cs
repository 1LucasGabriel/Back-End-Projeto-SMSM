using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IUnidadeSaudeRepository
    {
        Task<List<UnidadeSaude>> GetAll();
        Task<UnidadeSaude> GetById(int id);
        Task<UnidadeSaude> Post(UnidadeSaude unidadeSaude);
        Task<UnidadeSaude> Put(UnidadeSaude unidadeSaude, int id);
        Task<bool> Delete(int id);
    }
}
