using Domain.Models.Enums;

namespace Domain.Models.Request
{
    public class PerfilUsuarioRequest
    {
        public long IdUsuario { get; set; }
        public TipoNoticia TipoNoticia { get; set; }
    }
}
