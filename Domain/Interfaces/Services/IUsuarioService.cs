using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        Task<long> AdicionarUsuario(Usuario usuario);
        Task<Usuario> VisualizarUsuarioById(long idUsuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task DeletarUsuarioById(long idUsuario);
        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
