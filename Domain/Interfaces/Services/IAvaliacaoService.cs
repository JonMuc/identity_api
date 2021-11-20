using Domain.Models;
using Domain.Models.Enums;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAvaliacaoService
    {
        Task<long> AvaliarNoticia(long idUsuario, long idNoticia, TipoAvaliacao tipoAvaliacao);
        Task<long> AvaliarComentario(long idUsuario, long idComentario, TipoAvaliacao tipoAvaliacao);
        Task ExcluirAvaliacaoNoticia(long idUsuario, long idNoticia);
        Task ExcluirAvaliacaoComentario(long idUsuario, long idComentario);        
    }
}
