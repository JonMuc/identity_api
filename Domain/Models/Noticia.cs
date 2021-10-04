using Domain.Models.Enums;

namespace Domain.Models
{
    public class Noticia : BaseModel
    {
        public string Titulo { get; set; }
        public string UrlImage { get; set; }
        public string Fonte { get; set; }
        public string Link { get; set; }
        public string HoraAtras { get; set; }
        public TipoNoticia TipoNoticia { get; set; }
    }
}
