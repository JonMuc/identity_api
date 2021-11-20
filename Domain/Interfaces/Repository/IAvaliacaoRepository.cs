using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<long> AdicionarAvaliacaoAsync(Avaliacao request);
        Task<List<Avaliacao>> GetAvaliacaoByUsuarioNoticia(long idUsuario, long idNoticia);
        Task<List<Avaliacao>> GetAvaliacaoByUsuarioComentario(long idUsuario, long idComentario);
        Task ExcluirAvaliacaoNoticiaAsync(long idUsuario, long idNoticia);
        Task ExcluirAvaliacaoComentarioAsync(long idUsuario, long idComentario);
    }
}
