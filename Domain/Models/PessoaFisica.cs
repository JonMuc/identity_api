using System;

namespace Domain.Models
{
    public class PessoaFisica
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
