using Domain.Models;
using Domain.Services;

namespace Application.AppServices
{
    public class PessoaJuridicaAppService
    {
        private readonly IPessoaJuridicaService _pessoaJuridicaService;

        public PessoaJuridicaAppService(IPessoaJuridicaService pessoaJuridicaService)
        {
            _pessoaJuridicaService = pessoaJuridicaService;
        }

        public ResponseViewModel AdicionarPessoaJuridica(PessoaJuridica pessoa)
        {
            return _pessoaJuridicaService.AdicionarPessoaJuridica(pessoa);
        }

        public ResponseViewModel ListarPessoaJuridica()
        {
            return _pessoaJuridicaService.ListarPessoaJuridica();
        }

        public ResponseViewModel AtualizarPessoaJuridica(PessoaJuridica pessoa)
        {
            return _pessoaJuridicaService.AtualizarPessoaJuridica(pessoa);
        }

        public ResponseViewModel DeletarPessoaJuridica(PessoaJuridica pessoa)
        {
            return _pessoaJuridicaService.DeletarPessoaJuridica(pessoa);
        }

        public ResponseViewModel BuscarPessoaJuridica(long idPessoa)
        {
            return _pessoaJuridicaService.BuscarPessoaJuridica(idPessoa);
        }
    }
}
