using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task<long> AdicionarAsync(Comentario request);
        Task<Comentario> GetComentarioById(long idComentario);
        Task<IEnumerable<Comentario>> ListarComentariosComentarioAsync(long idComentario);
        Task<IEnumerable<ViewComentario>> ListarComentariosNoticiaAsync(ComentarioRequest request);
    }
}
