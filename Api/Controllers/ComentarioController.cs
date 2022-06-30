using Api.Config;
using Application.AppServices;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("comentario")]
    public class ComentarioController : BaseController
    {
        private readonly ComentarioAppService _comentarioAppService;

        public ComentarioController(ComentarioAppService comentarioAppService)
        {
            _comentarioAppService = comentarioAppService;
        }

        [ValidateUser]
        [HttpPost("salvar-comentario-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SalvarComentarioNoticiaAsync([FromBody] Comentario request)
        {
            request.IdCriadoPor = ObterUsuario().Id;
            var response = await _comentarioAppService.SalvarComentarioNoticiaAsync(request);
            return Ok(response);
        }

        [ValidateUser]
        [HttpPost("comentar-comentario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ComentarComentarioAsync([FromBody] Comentario request)
        {
            request.IdCriadoPor = ObterUsuario().Id;
            var response = await _comentarioAppService.ComentarComentarioAsync(request);
            return Ok(response);
        }

        [ValidateUser]
        [HttpGet("obter-comentario-noticia/{idNoticia:int}/{pageIndex}/{pageSize}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosNoticiaAsync(int idNoticia, int pageIndex, int pageSize)
        {
            var response = await _comentarioAppService.ListarComentariosNoticiaAsync(
                new ComentarioRequest() { IdNoticia = idNoticia, IdUsuario = ObterUsuario().Id, pageIndex = pageIndex, pageSize = pageSize});
            return Ok(response);
        }

        [HttpGet("obter-comentario-noticia-deslogado/{idNoticia:int}/{pageIndex}/{pageSize}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosNoticiaDeslogadoAsync(int idNoticia, int pageIndex, int pageSize)
        {
            var response = await _comentarioAppService.ListarComentariosNoticiaDeslogadoAsync(
                new ComentarioRequest() { IdNoticia = idNoticia, pageIndex = pageIndex, pageSize = pageSize });
            return Ok(response);
        }

        [ValidateUser]
        [HttpGet("obter-comentarios-de-comentario/{idComentario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosComentarioAsync(int idComentario)
        {
            var request = new ComentarioRequest { IdComentario = idComentario, IdUsuario = ObterUsuario().Id };
            var response = await _comentarioAppService.ListarComentariosComentarioAsync(request, 0, 0);
            return Ok(response);
        }

        [HttpGet("obter-comentarios-de-comentario-deslogado/{idComentario:int}/{pageIndex}/{pageSize}")] //testar
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarComentariosComentarioDeslogadoAsync(int idComentario, int pageIndex, int pageSize)
        {
            var response = await _comentarioAppService.ListarComentariosComentarioDeslogadoAsync(idComentario, pageIndex, pageSize);
            return Ok(response);
        }


        [ValidateUser]
        [HttpGet("excluir-comentario-noticia/{idComentario:long}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ExcluirComentarioNoticiaAsync(long idComentario)
        {
            var response = await _comentarioAppService.ExcluirComentarioNoticiaAsync(new Comentario() { Id = idComentario, IdCriadoPor = ObterUsuario().Id });
            return Ok(response);
        }
    }
}
