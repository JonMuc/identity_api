using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("avaliacao")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AvaliacaoAppService _avaliacaoAppService;

        public AvaliacaoController(AvaliacaoAppService avaliacaoAppService)
        {
            _avaliacaoAppService = avaliacaoAppService;
        }

        [HttpPost("noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AvaliarNoticia(AvaliacaoRequest request)
        {
            var response = await _avaliacaoAppService.AvaliarNoticia(request);
            return Ok(response);
        }

        [HttpPost("comentario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AvaliarComentario(AvaliacaoRequest request)
        {
            var response = await _avaliacaoAppService.AvaliarComentario(request);
            return Ok(response);
        }

        [HttpPost("excluir-avaliacao-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ExcluirAvaliacaoNoticia(AvaliacaoRequest request)
        {
            var response = await _avaliacaoAppService.ExcluirAvaliacaoNoticia(request.IdUsuario, request.IdNoticia);
            return Ok(response);
        }

        [HttpPost("excluir-avaliacao-comentario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ExcluirAvaliacaoComentario(AvaliacaoRequest request)
        {
            var response = await _avaliacaoAppService.ExcluirAvaliacaoComentario(request.IdUsuario, request.IdComentario);
            return Ok(response);
        }

    }
}
