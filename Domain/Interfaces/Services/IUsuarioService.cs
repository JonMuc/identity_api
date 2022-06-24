using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        Task<long> AdicionarUsuario(Usuario usuario);
        Task<Usuario> VisualizarUsuarioById(long idUsuario);
        Task<IEnumerable<Usuario>> VisualizarSeguidores(long idUsuario);
        Task<IEnumerable<Usuario>> VisualizarSeguindo(long idUsuario);
        Task<Usuario> VisualizarPerfilUsuario(long idUsuario);
        Task<IEnumerable<Usuario>> BuscarUsuario(string nomeUsuario, int pageIndex, int pageSize);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task DeletarUsuarioById(long idUsuario);
        Task<Usuario> CriarUsuarioStep(CriarContaUsuario usuario);
        Task<string> UploadImagemAsync(UploadImagemRequest request);
        Task<long> SeguirUsuario(long idUsuarioSeguido, long idUsuarioSeguidor);
        Task<long> DeseguirUsuario(long idUsuarioDeseguido, long idUsuarioDeseguindo);

        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
