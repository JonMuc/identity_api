using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Request;
using Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Criptografia;

namespace Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> LoginAsync(LoginRequest request)
        {
            var errosResponse = new List<string>(0);
            var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(request.Email);
            if (usuario == null)
            {
                errosResponse.Add("Email não encontrado.");
                throw new ParametroException(errosResponse);
            }
            if (CryptographyHelper.DecryptString(usuario.Senha) != request.Senha)
            {
                errosResponse.Add("Email e senha não conferem.");
                throw new ParametroException(errosResponse);
            }
            return usuario;
        }
    }
}
