using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
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
        Task<string> UploadImagemAsync(UploadImagemRequest request);

        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
