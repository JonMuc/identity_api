using Domain.Models;
using Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task<long> AdicionarAsync(Comentario request);
        Task<IEnumerable<ViewComentario>> ListarComentariosNoticiaAsync(long idUsuarioa);
        Task<IEnumerable<Comentario>> ListarComentariosComentarioAsync(long idComentario);
    }
}
