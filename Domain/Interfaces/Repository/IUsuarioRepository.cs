using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<long> AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> GetUsuarioById(long idUsuarioa);
        Task DeletarUsuarioAsync(long idUsuario);
        IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario);
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);
        Task<bool> VerificarExistenciaEmail(string email);
        Task<bool> LoginAsync(Usuario request);
        Task<Usuario> GetUsuarioByEmailAsync(string email);

    }
}
