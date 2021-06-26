using Domain.Models;

namespace Domain.Services
{
    public interface IPessoaFisicaService
    {
        ResponseViewModel AdicionarPessoaFisica(PessoaFisica pessoa);
        ResponseViewModel ListarPessoaFisica();
        ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa);
        ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa);
        ResponseViewModel BuscarPessoaFisica(long idPessoa);
    }
}
