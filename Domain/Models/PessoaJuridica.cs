namespace Domain.Models
{
    public class PessoaJuridica
    {
        public long Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Email { get; set; }
    }
}
