using Domain.Models.Enums;

namespace Domain.Models
{
    public class Avaliacao : BaseModel
    {
        public long? IdComentario { get; set; }
        public long? IdNoticia { get; set; }
        public long IdUsuario { get; set; }
        public TipoAvaliacao TipoAvaliacao { get; set; }
    }
}
