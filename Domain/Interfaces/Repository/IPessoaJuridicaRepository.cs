using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPessoaJuridicaRepository
    {
        PessoaJuridica AdicionarPessoaJuridica(PessoaJuridica pessoa);
        PessoaJuridica AtualizarPessoaJuridica(PessoaJuridica pessoa);
        bool DeletarPessoaJuridica(PessoaJuridica pessoa);
        PessoaJuridica BuscarPessoaJuridica(long idPessoa);
        List<PessoaJuridica> ListarPessoaJuridica();
    }
}
