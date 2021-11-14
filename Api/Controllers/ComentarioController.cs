using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("comentario")]
    public class ComentarioController : ControllerBase
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


        [HttpGet("obter-comentario-noticia/{idNoticia:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosNoticiaAsync(int idNoticia)
        {
            var response = await _comentarioAppService.ListarComentariosNoticiaAsync(idNoticia);
            return Ok(response);
        }
    }
}
