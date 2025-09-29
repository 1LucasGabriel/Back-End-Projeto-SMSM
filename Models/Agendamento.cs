namespace APIProjeto.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int IdDemanda { get; set; }
        public int IdVaga { get; set; }
        public int IdUsuarioRegulador { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string StatusComparecimento { get; set; }
    }
}
