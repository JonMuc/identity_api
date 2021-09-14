using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCrud.Controllers
{
    [ApiController, Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly NoticiaAppService _informacaoGoogleAppService;

        public UsuarioController(NoticiaAppService informacaoGoogleAppService)
        {
            _informacaoGoogleAppService = informacaoGoogleAppService;
        }

        [HttpPost("salvar-usuario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult SalvarUsuario([FromBody] Usuario requestViewModel)
        {
            var response = _informacaoGoogleAppService.ListarManchete();
            return Ok(response);
        }
    }
}
