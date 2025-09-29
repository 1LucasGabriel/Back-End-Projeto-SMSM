using APIProjeto.Data;
using APIProjeto.Models;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MyDbContext _dbContext;
        public UsuarioRepository(MyDbContext projetoDbContext)
        {
            _dbContext = projetoDbContext;
        }
        public async Task<Usuario> GetById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<Usuario> Post(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }
        public async Task<Usuario> Put(Usuario usuario, int id)
        {
            Usuario usuarioPorId = await GetById(id);
            if (usuarioPorId == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Cpf = usuario.Cpf;
            usuarioPorId.Senha = usuario.Senha;
            usuarioPorId.IdUnidadeSaude = usuario.IdUnidadeSaude;
            usuarioPorId.IdPerfil = usuario.IdPerfil;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Delete(int id)
        {
            Usuario usuarioPorId = await GetById(id);
            if (usuarioPorId == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
