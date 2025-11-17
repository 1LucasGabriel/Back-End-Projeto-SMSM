using APIProjeto.Models;

namespace APIProjeto.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
    }
}
