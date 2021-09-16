using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCrud.Controllers
{
    [ApiController, Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioAppService _usuarioAppService;

        public UsuarioController(UsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost("salvar-usuario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult SalvarUsuario([FromBody] Usuario usuario)
        {
            var response = _usuarioAppService.AdicionarUsuario(usuario);
            return Ok(response);
        }
    }
}
