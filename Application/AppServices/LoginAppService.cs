using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Request;
using Domain.Services;
using System.Threading.Tasks;

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
            return new ResponseViewModel { Sucesso = true, Objeto = response };
        }
    }
}
