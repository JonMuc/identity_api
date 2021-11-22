using Domain.Models.Enums;
using System;

namespace Domain.Models.Dto
{
    public class ViewNoticia
    {
        public long IdNoticia { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Titulo { get; set; }
        public string UrlImage { get; set; }
        public string Fonte { get; set; }
        public TipoNoticia TipoNoticia { get; set; }
        public OrigemNoticia OrigemNoticia { get; set; }
        public long QuantidadeLike { get; set; }
        public long QuantidadeDeslike { get; set; }
        public TipoAvaliacao? UsuarioAvaliacao { get; set; }
        public long IdAvaliacao { get; set; }
        public bool NoticiaFavorito { get; set; }
        public long IdFavorito { get; set; }
    }
}
