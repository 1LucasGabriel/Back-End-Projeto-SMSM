using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IPerfilRepository
    {
        Task<List<Perfil>> GetAll();
        Task<Perfil> GetById(int id);
        Task<Perfil> Post(Perfil perfil);
        Task<Perfil> Put(Perfil perfil, int id);
        Task<bool> Delete(int id);
    }
}
