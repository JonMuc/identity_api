using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> AdicionarUsuarioAsync(Usuario usuario);
        //PessoaFisica AtualizarPessoaFisica(PessoaFisica pessoa);
        //bool DeletarPessoaFisica(PessoaFisica pessoa);
        IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario);
        //List<PessoaFisica> ListarPessoaFisica();
    }
}
