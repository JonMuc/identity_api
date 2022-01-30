using Api.Config;
using Application.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, ValidateUser, Route("push-notification")]
    public class PushController : BaseController
    {
        private readonly IPushService _pushService;

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
    }
}
