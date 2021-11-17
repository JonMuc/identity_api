using Application.AppServices;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginAppService _loginAppService;

        public LoginController(LoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }


        [HttpPost("logar")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SalvarUsuarioStep([FromBody] LoginRequest request)
        {
            var response = await _loginAppService.Login(request);
            return Ok(response);
        }
    }
}
