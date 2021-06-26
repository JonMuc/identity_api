using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPessoaFisicaRepository
    {
        PessoaFisica AdicionarPessoaFisica(PessoaFisica pessoa);
        PessoaFisica AtualizarPessoaFisica(PessoaFisica pessoa);
        bool DeletarPessoaFisica(PessoaFisica pessoa);
        PessoaFisica BuscarPessoaFisica(long idPessoa);
        List<PessoaFisica> ListarPessoaFisica();
    }
}
