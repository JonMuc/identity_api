using System;

namespace Domain.Models.Dto
{
    public class ViewComentario
    {
        public long IdComentario { get; set; }
        public long IdUsuario { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public DateTime DataComentario { get; set; }
    }
}
