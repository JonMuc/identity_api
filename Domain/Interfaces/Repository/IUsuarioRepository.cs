using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<long> SeguirUsuario(CrzSeguirUsuario usuario);
        Task<IEnumerable<Usuario>> VisualizarSeguidores(long idUsuario);
        Task<IEnumerable<Usuario>> VisualizarSeguindo(long idUsuario);
        Task<Usuario> VisualizarPerfilUsuario(long idUsuario);
        Task<long> DeseguirUsuario(long idUsuarioDeseguido, long idUsuarioDeseguindo);
        Task<long> AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> GetUsuarioById(long idUsuarioa);
        Task<IEnumerable<Usuario>> BuscarUsuario(DataRequest request);
        Task DeletarUsuarioAsync(long idUsuario);
        IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario);
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);
        Task<bool> VerificarExistenciaEmail(string email);
        Task<bool> LoginAsync(Usuario request);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task<bool> VerificarExistenciaNomeUser(string nomeUser);
        Task AtualizarTokenPush(long idUsuario, string tokenPush);
    }
}
