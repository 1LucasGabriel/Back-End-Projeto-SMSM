using System.ComponentModel.DataAnnotations;

namespace APIProjeto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; } 
        public int IdUnidadeSaude { get; set; }
        public int IdPerfil { get; set; }

    }
}
