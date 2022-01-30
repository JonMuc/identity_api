using Api.Config;
using Application.AppServices;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, ValidateUser, Route("comentario")]
    public class ComentarioController : BaseController
    {
        private readonly ComentarioAppService _comentarioAppService;

        public ComentarioController(ComentarioAppService comentarioAppService)
        {
            _comentarioAppService = comentarioAppService;
        }

        [HttpPost("salvar-comentario-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SalvarComentarioNoticiaAsync([FromBody] Comentario request)
        {
            var response = await _comentarioAppService.SalvarComentarioNoticiaAsync(request);
            return Ok(response);
        }

        [HttpPost("comentar-comentario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ComentarComentarioAsync([FromBody] Comentario request)
        {
            var response = await _comentarioAppService.ComentarComentarioAsync(request);
            return Ok(response);
        }


        [HttpPost("obter-comentario-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosNoticiaAsync([FromBody] ComentarioRequest request)
        {
            var response = await _comentarioAppService.ListarComentariosNoticiaAsync(request);
            return Ok(response);
        }

        [HttpPost("obter-comentarios-de-comentario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosComentarioAsync([FromBody] ComentarioRequest request)
        {
            var response = await _comentarioAppService.ListarComentariosComentarioAsync(request);
            return Ok(response);
        }
    }
}
