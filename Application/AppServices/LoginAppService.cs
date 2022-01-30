using Application.ModelsDto;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
using Domain.Services;
using System.Threading.Tasks;
using Util.Jwt;

namespace Application.AppServices
{
    public class LoginAppService : BaseAppService
    {
        private readonly ILoginService _loginService;

        public LoginAppService(IUnitOfWork unitOfWork, ILoginService loginService) : base(unitOfWork)
        {
            _loginService = loginService;
        }

        public async Task<ResponseViewModel> Login(LoginRequest request)
        {
            var response = await _loginService.LoginAsync(request);
            var token = JWTManager.GenerateToken(response);
            var result = new LoginResponse { Id = response.Id, Descricao = response.Descricao, Email = response.Email, Foto = response.Foto,
            Nome = response.Nome, IdGoogle = response.IdGoogle, IdFacebook = response.IdFacebook, PerfilInstagram = response.PerfilInstagram,
            PerfilLinkedin = response.PerfilLinkedin, TokenPush = response.TokenPush, StatusRegistro = response.StatusRegistro, PerfilTwitter = response.PerfilTwitter,
            Telefone = response.Telefone, Token = token};
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }
    }
}
