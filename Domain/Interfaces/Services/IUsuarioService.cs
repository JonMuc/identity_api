using Domain.Models;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        ResponseViewModel AdicionarUsuario(Usuario usuario);
        //ResponseViewModel ListarPessoaFisica();
        //ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa);
        //ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa);
        //ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
