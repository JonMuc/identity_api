using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Request;
using Domain.Validations;
using System.Threading.Tasks;
using Util.Jwt;

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

        public async Task<Usuario> LoginAsync(LoginRequest request)
        {
            return await _loginValidation.LoginUsuarioAsync(request);
        }
    }
}
