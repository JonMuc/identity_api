using Application.AppServices;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("noticia")]
    public class NoticiaController : ControllerBase
    {
        private readonly NoticiaAppService _noticiaAppService;

        public NoticiaController(NoticiaAppService noticiaAppService)
        {
            _noticiaAppService = noticiaAppService;
        }

        [HttpPost("listar-manchete")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarManchete([FromBody] NoticiaRequest request)
        {
            var response = await _noticiaAppService.ListarManchete(request);
            return Ok(response);
        }

        [HttpPost("listar-noticias")] //com a query melhorada
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarNoticias([FromBody] NoticiaRequest request)
        {
            var response = await _noticiaAppService.ListarNoticias(request);
            return Ok(response);
        }

        [HttpGet("listar-noticia-por-tipo")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarPorTipo(NoticiaRequest tipoNoticia)
        {
            var response = await _noticiaAppService.ListarNoticiaPorTipo(tipoNoticia);
            return Ok(response);
        }

        [HttpPost("incluir-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Salvar([FromBody] Noticia noticia)
        {
            var response = await _noticiaAppService.AdicionarNoticia(noticia);
            return Ok(response);
        }

        [HttpPost("atualizar-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] Noticia noticia)
        {
            var response = await _noticiaAppService.AtualizarNoticia(noticia);
            return Ok(response);
        }

        [HttpDelete("excluir-noticia/{idNoticia:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deletar(int idNoticia)
        {
            var response = await _noticiaAppService.DeletarNoticiaById(idNoticia);
            return Ok(response);
        }

        [HttpGet("visualizar-noticia/{idNoticia:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visualizar(int idNoticia)
        {
            var response = await _noticiaAppService.VisualizarNoticiaById(idNoticia);
            return Ok(response);
        }
    }
}
