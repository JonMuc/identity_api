namespace Domain.Models
{
    public class Usuario : BaseModel
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public string IdGoogle { get; set; }
        public string IdFacebook { get; set; }
        public string PerfilLinkedin { get; set; }
        public string PerfilInstagram { get; set; }
        public string PerfilTwitter { get; set; }
        public string PerfilFacebook{ get; set; }
        public string Descricao { get; set; }
        public string TokenPush { get; set; }
        public int quantidadeSeguidores { get; set; }
        public int quantidadeSeguindo { get; set; }
    }
}
