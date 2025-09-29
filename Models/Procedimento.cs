namespace APIProjeto.Models
{
    public class Procedimento
    {
        public int Id { get; set; }
        public string CodigoProcedimento { get; set; }
        public string NomeProcedimento { get; set; }
        public string Tipo { get; set; }
        public int IdEspecialidade { get; set; }
    }
}
