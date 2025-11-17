using APIProjeto.Models;

namespace APIProjeto.Repositories.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<List<Agendamento>> GetAll();
        Task<Agendamento> GetById(int id);
        Task<Agendamento> Post(Agendamento agendamento);
        Task<Agendamento> Put(Agendamento agendamento, int id);
        Task<bool> Delete(int id);
        Task<List<dynamic>> GetAllComposto();
    }
}
