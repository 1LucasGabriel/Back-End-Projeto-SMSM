﻿using System.ComponentModel.DataAnnotations;

namespace APIProjeto.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; } 
        public string Cns { get; set; } 
        public DateTime DataNascimento { get; set; }
        public string? Endereco { get; set; }

    }
}
