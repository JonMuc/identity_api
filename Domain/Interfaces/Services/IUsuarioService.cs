using Domain.Models;
using Domain.Models.Dto;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        Task<long> AdicionarUsuario(Usuario usuario);
        Task<Usuario> VisualizarUsuarioById(long idUsuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task DeletarUsuarioById(long idUsuario);
        Task<Usuario> CriarUsuarioStep(CriarContaUsuario usuario);

        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
