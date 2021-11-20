using Domain.Models.Enums;

namespace Domain.Models
{
    public class AvaliacaoRequest
    {
        public long IdUsuario { get; set; }
        public long IdNoticia { get; set; }
        public long IdComentario { get; set; }
        public TipoAvaliacao TipoAvaliacao { get; set; }
    }
}
