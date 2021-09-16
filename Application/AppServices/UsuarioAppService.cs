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

        public ResponseViewModel AdicionarUsuario(Usuario usuario)
        {
            return _usuarioService.AdicionarUsuario(usuario);
        }

    }
}
