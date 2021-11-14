using System;

namespace Domain.Models.Dto
{
    public class ViewComentario
    {
        public long IdComentario { get; set; }
        public long IdUsuario { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        public DateTime DataComentario { get; set; }
        public long QuantidadeLike { get; set; }
        public long QuantidadeDeslike { get; set; }
        public bool ComentarioFilho { get; set; }
    }
}
