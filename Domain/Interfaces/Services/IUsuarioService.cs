using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        Task<int> AdicionarUsuario(Usuario usuario);
        //ResponseViewModel ListarPessoaFisica();
        //ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa);
        //ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa);
        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
