using Domain.Models;
using Domain.Services;

namespace Application.AppServices
{
    public class PessoaFisicaAppService
    {
        private readonly IPessoaFisicaService _pessoaFisicaService;

        public PessoaFisicaAppService(IPessoaFisicaService pessoaFisicaService)
        {
            _pessoaFisicaService = pessoaFisicaService;
        }

        public ResponseViewModel AdicionarPessoaFisica(PessoaFisica pessoa)
        {
            return _pessoaFisicaService.AdicionarPessoaFisica(pessoa);
        }

        public ResponseViewModel ListarPessoaFisica()
        {
            return _pessoaFisicaService.ListarPessoaFisica();
        }

        public ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa)
        {
            return _pessoaFisicaService.AtualizarPessoaFisica(pessoa);
        }

        public ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa)
        {
            return _pessoaFisicaService.DeletarPessoaFisica(pessoa);
        }

        public ResponseViewModel BuscarPessoaFisica(long idPessoa)
        {
            return _pessoaFisicaService.BuscarPessoaFisica(idPessoa);
        }
    }
}
