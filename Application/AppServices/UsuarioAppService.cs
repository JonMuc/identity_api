using Domain.Models;
using Domain.Services;

namespace Application.AppServices
{
    public class UsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ResponseViewModel AdicionarPessoaFisica(PessoaFisica pessoa)
        {
            return _usuarioService.AdicionarPessoaFisica(pessoa);
        }

        public ResponseViewModel ListarPessoaFisica()
        {
            return _usuarioService.ListarPessoaFisica();
        }

        public ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa)
        {
            return _usuarioService.AtualizarPessoaFisica(pessoa);
        }

        public ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa)
        {
            return _usuarioService.DeletarPessoaFisica(pessoa);
        }

        public ResponseViewModel BuscarPessoaFisica(long idPessoa)
        {
            return _usuarioService.BuscarPessoaFisica(idPessoa);
        }
    }
}
