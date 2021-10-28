namespace Domain.Models
{
    public class Comentario : BaseModel
    {
        public string Mensagem { get; set; }
        public long IdNoticia { get; set; }
        // SE EXISTIR É UM COMENTARIO DE COMENTARIO
        public long IdComentario { get; set; }

    }
}
