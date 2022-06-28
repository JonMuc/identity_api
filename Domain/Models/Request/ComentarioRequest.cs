namespace Domain.Models.Request
{
    public class ComentarioRequest
    {
        public long IdUsuario { get; set; }
        public long IdNoticia { get; set; }
        public long IdComentario { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
}
