

namespace Domain.Models
{
    public class NoticiaFavorito : BaseModel
    {
        public long IdNoticia { get; set; }
        public long IdUsuario { get; set; }   
        public Noticia Noticia { get; set; } //COLOQUEI SO PARA CONSEGUIR GUARDAR QUAL NOTICIA E QUAL USUARIO VINCULADO (visualizar NoticiaFavorito)
        public Usuario Usuario { get; set; } //COLOQUEI SO PARA CONSEGUIR GUARDAR QUAL NOTICIA E QUAL USUARIO VINCULADO (visualizar NoticiaFavorito)

    }
}
