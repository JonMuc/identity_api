using Domain.Models.Enums;
using Domain.Models.Request;

namespace Domain.Models
{
    public class PerfilUsuario : BaseModel
    {
        public TipoNoticia TipoNoticia { get; set; }
        public long IdUsuario { get; set; }        

        public PerfilUsuario toModel(PerfilUsuarioRequest request)
        {
            var obj = new PerfilUsuario()
            {
                IdUsuario = request.IdUsuario,
                TipoNoticia = request.TipoNoticia
            };
            return obj;
        }
    }
}
