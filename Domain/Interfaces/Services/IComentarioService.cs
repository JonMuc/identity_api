using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IComentarioService
    {
        Task ExcluirComentarioNoticiaAsync(Comentario request);
        Task<Comentario> AdicionarComentarioNoticiaAsync(Comentario request);
        Task<Comentario> ComentarComentarioAsync(Comentario request);
    }
}
