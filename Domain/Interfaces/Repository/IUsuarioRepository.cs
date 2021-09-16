using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario AdicionarUsuario(Usuario usuario);
        //PessoaFisica AtualizarPessoaFisica(PessoaFisica pessoa);
        //bool DeletarPessoaFisica(PessoaFisica pessoa);
        //IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario);
        //List<PessoaFisica> ListarPessoaFisica();
    }
}
