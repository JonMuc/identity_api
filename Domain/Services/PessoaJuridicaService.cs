using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;

namespace Domain.Services
{
    public class PessoaJuridicaService : IPessoaJuridicaService
    {
        private readonly PessoaJuridicaValidation _pessoaJuridicaValidation;
        private readonly IPessoaJuridicaRepository _pessoaJuridicaRepository;

        public PessoaJuridicaService(PessoaJuridicaValidation pessoaJuridicaValidation, IPessoaJuridicaRepository pessoaJuridicaRepository)
        {
            _pessoaJuridicaValidation = pessoaJuridicaValidation;
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
        }

        public ResponseViewModel AdicionarPessoaJuridica(PessoaJuridica pessoa)
        {
            _pessoaJuridicaValidation.ValidarSalvarPessoaJuridica(pessoa);
            var result = _pessoaJuridicaRepository.AdicionarPessoaJuridica(pessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel ListarPessoaJuridica()
        {
            var result = _pessoaJuridicaRepository.ListarPessoaJuridica();
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel AtualizarPessoaJuridica(PessoaJuridica pessoa)
        {
            _pessoaJuridicaValidation.ValidarSalvarPessoaJuridica(pessoa);
            var result = _pessoaJuridicaRepository.AtualizarPessoaJuridica(pessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel DeletarPessoaJuridica(PessoaJuridica pessoa)
        {
            var result = _pessoaJuridicaRepository.DeletarPessoaJuridica(pessoa);
            return new ResponseViewModel { Sucesso = result, Objeto = pessoa };
        }

        public ResponseViewModel BuscarPessoaJuridica(long idPessoa)
        {
            var result = _pessoaJuridicaRepository.BuscarPessoaJuridica(idPessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }
    }
}
