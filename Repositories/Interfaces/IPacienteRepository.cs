using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Task<List<Paciente>> GetAll();
        Task<Paciente> GetById(int id);
        Task<Paciente> Post(Paciente paciente);
        Task<Paciente> Put(Paciente paciente, int id);
        Task<bool> Delete(int id);
    }
}
