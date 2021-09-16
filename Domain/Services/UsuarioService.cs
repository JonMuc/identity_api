using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;

namespace Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioValidation _usuarioValidation;        
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioValidation usuarioValidation, IUsuarioRepository usuarioRepository)
        {
            _usuarioValidation = usuarioValidation;           
            _usuarioRepository = usuarioRepository;
        }

        public ResponseViewModel AdicionarUsuario(Usuario usuario)
        {
            _usuarioValidation.ValidarSalvarUsuario(usuario);
            var result = _usuarioRepository.AdicionarUsuario(usuario);
            return new ResponseViewModel { Sucesso = true, Objeto = result };            
        }

        //public ResponseViewModel ListarPessoaFisica()
        //{
        //    var result = _usuarioRepository.BuscarUsuarioPorId(1);
        //    // var result = _pessoaFisicaRepository.ListarPessoaFisica();
        //    return new ResponseViewModel { Sucesso = true, Objeto = result };
        //}

        //public ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa)
        //{
        //    _pessoaFisicaValidation.ValidarSalvarPessoaFisica(pessoa);
        //    var result = _pessoaFisicaRepository.AtualizarPessoaFisica(pessoa);
        //    return new ResponseViewModel { Sucesso = true, Objeto = result };
        //}

        //public ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa)
        //{
        //    var result = _pessoaFisicaRepository.DeletarPessoaFisica(pessoa);
        //    return new ResponseViewModel { Sucesso = result, Objeto = pessoa };
        //}

        //public ResponseViewModel BuscarPessoaFisica(long idPessoa)
        //{
        //    var result = _pessoaFisicaRepository.BuscarPessoaFisica(idPessoa);
        //    return new ResponseViewModel { Sucesso = true, Objeto = result };
        //}
    }
}
