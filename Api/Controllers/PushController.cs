using Application.AppServices;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("push-notification")]
    public class PushController : ControllerBase
    {
        private readonly IPushService _pushService;
        private readonly IUsuarioRepository _usuarioRepository;

        public PushController(IPushService pushService)
        {
            _pushService = pushService;
        }               

        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EnviarPushNotification(long idUsuario)
        {
            await _pushService.EnviarPush(new Usuario { Id = idUsuario });
            var response = "deu bom";
            return Ok(response);
        }

        [HttpPost("getToken")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetToken(long idUsuario, string tokenPush)
        {
            await _usuarioRepository.AtualizarTokenPush(idUsuario, tokenPush);
            var response = "deu bom";
            return Ok(response);
        }
    }
}
