using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;

namespace Domain.Services
{
    public class PessoaFisicaService : IPessoaFisicaService
    {
        private readonly PessoaFisicaValidation _pessoaFisicaValidation;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PessoaFisicaService(PessoaFisicaValidation pessoaFisicaValidation, IPessoaFisicaRepository pessoaFisicaRepository, IUsuarioRepository usuarioRepository)
        {
            _pessoaFisicaValidation = pessoaFisicaValidation;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public ResponseViewModel AdicionarPessoaFisica(PessoaFisica pessoa)
        {
            _pessoaFisicaValidation.ValidarSalvarPessoaFisica(pessoa);
            var result = _pessoaFisicaRepository.AdicionarPessoaFisica(pessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel ListarPessoaFisica()
        {
            var result = _usuarioRepository.BuscarUsuarioPorId(1);
            // var result = _pessoaFisicaRepository.ListarPessoaFisica();
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa)
        {
            _pessoaFisicaValidation.ValidarSalvarPessoaFisica(pessoa);
            var result = _pessoaFisicaRepository.AtualizarPessoaFisica(pessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa)
        {
            var result = _pessoaFisicaRepository.DeletarPessoaFisica(pessoa);
            return new ResponseViewModel { Sucesso = result, Objeto = pessoa };
        }

        public ResponseViewModel BuscarPessoaFisica(long idPessoa)
        {
            var result = _pessoaFisicaRepository.BuscarPessoaFisica(idPessoa);
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }
    }
}
