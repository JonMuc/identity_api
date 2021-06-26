using Domain.Models;

namespace Domain.Services
{
    public interface IPessoaJuridicaService
    {
        ResponseViewModel AdicionarPessoaJuridica(PessoaJuridica pessoa);
        ResponseViewModel ListarPessoaJuridica();
        ResponseViewModel AtualizarPessoaJuridica(PessoaJuridica pessoa);
        ResponseViewModel DeletarPessoaJuridica(PessoaJuridica pessoa);
        ResponseViewModel BuscarPessoaJuridica(long idPessoa);
    }
}
