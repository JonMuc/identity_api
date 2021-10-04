using Application.AppServices;
using Domain.Models;
using Domain.Models.Enums;
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

        [HttpGet("listar-manchete")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ListarManchete()
        {
            var response = _noticiaAppService.ListarManchete();
            return Ok(response);
        }

        [HttpGet("listar-noticia-por-tipo")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarPorTipo(TipoNoticia tipoNoticia)
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
