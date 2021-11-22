using Domain.Models.Enums;

namespace Domain.Models.Request
{
    public class NoticiaRequest : DataRequest
    {
        public TipoNoticia TipoNoticia { get; set; }
        public long IdUsuario { get; set; } 
    }
}
