namespace APIProjeto.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public int IdProcedimento { get; set; }
        public int IdUnidadeOfertante { get; set; }
        public int Quantidade { get; set; }
        public string MesAnoReferencia { get; set; }
    }
}
