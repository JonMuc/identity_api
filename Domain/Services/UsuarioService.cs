using Domain.Interfaces;
using Domain.Models;
using Domain.Util;
using Domain.Validations;
using System.Threading.Tasks;

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

        public async Task<long> AdicionarUsuario(Usuario usuario)
        {
            _usuarioValidation.ValidarSalvarUsuario(usuario);
            var result = await _usuarioRepository.AdicionarUsuarioAsync(usuario);
            return result;           
        }

        public async Task<Usuario> AtualizarUsuario(Usuario edit)
        {
            
            var user = await _usuarioRepository.GetUsuarioById(edit.Id);
            _usuarioValidation.VerificarExistenciaUsuario(user);

            var usuario = _usuarioValidation.CompararUsuario(edit, user);
            _usuarioValidation.ValidarSalvarUsuario(usuario);

            var result = await _usuarioRepository.AtualizarUsuarioAsync(usuario);

            return result;
        }

        public async Task DeletarUsuarioById(long idUsuario)
        {
            var result = await _usuarioRepository.GetUsuarioById(idUsuario);
            _usuarioValidation.VerificarExistenciaUsuario(result);

            await _usuarioRepository.DeletarUsuarioAsync(idUsuario);            
        }

        public async Task<Usuario> VisualizarUsuarioById(long idUsuario)
        {            
            var result = await _usuarioRepository.GetUsuarioById(idUsuario);
            _usuarioValidation.VerificarExistenciaUsuario(result);
            return result;
        }
    }
}
