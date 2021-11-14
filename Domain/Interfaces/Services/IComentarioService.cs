using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IComentarioService
    {
        Task<Comentario> AdicionarComentarioNoticiaAsync(Comentario request);
        Task<Comentario> ComentarComentarioAsync(Comentario request);
    }
}
