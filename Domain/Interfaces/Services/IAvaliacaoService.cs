using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAvaliacaoService
    {
        Task<Comentario> AdicionarAsync(Comentario request);
        //Task<Usuario> VisualizarUsuarioById(long idUsuario);
        //Task<Usuario> AtualizarUsuario(Usuario usuario);
        //Task DeletarUsuarioById(long idUsuario);
        //Task<Usuario> CriarUsuarioStep(CriarContaUsuario usuario);
    }
}
