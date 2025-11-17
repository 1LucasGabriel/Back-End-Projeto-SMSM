using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByCpfAsync(string cpf);
        Task<Usuario> Post(Usuario usuario);
        Task<Usuario> Put(Usuario usuario, int id);
        Task<bool> Delete(int id);
    }
}
