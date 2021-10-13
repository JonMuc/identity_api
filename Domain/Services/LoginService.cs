using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly LoginValidation _loginValidation;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginService(LoginValidation loginValidation, IUsuarioRepository usuarioRepository)
        {
            _loginValidation = loginValidation;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> LoginAsync(Usuario usuario)
        {
            return await _loginValidation.LoginUsuarioAsync(usuario);
        }
    }
}
