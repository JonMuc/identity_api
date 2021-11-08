using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<long> AdicionarAsync(Comentario request);
        Task<IEnumerable<Comentario>> ListarComentariosNoticiaAsync(long idUsuarioa);
    }
}
