namespace APIProjeto.Models
{
    public class Demanda
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdProcedimento { get; set; }
        public int IdUnidadeSolicitante { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public string Justificativa { get; set; }
    }
}
