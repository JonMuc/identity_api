using Application.AppServices;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("noticia-favoritos")]
    public class NoticiaFavoritoController : ControllerBase
    {
        private readonly NoticiaFavoritoAppService _noticiaFavoritoAppService;

        public NoticiaFavoritoController(NoticiaFavoritoAppService noticiaFavoritoAppService)
        {
            _noticiaFavoritoAppService = noticiaFavoritoAppService;
        }

        [HttpGet("listar-favoritos")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarFavoritos(long idUsuario)
        {
            var response = await _noticiaFavoritoAppService.ListarNoticiaFavorito(idUsuario);
            return Ok(response);
        }

        [HttpPost("incluir")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Salvar([FromBody] NoticiaFavorito noticiaFavorito)
        {
            var response = await _noticiaFavoritoAppService.AdicionarNoticiaFavorito(noticiaFavorito);
            return Ok(response);
        }

        [HttpPost("atualizar")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] NoticiaFavorito noticiaFavorito)
        {
            var response = await _noticiaFavoritoAppService.AtualizarNoticiaFavorito(noticiaFavorito);
            return Ok(response);
        }

        [HttpDelete("excluir/{idNoticiaFavorito:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deletar(int idNoticiaFavorito)
        {
            var response = await _noticiaFavoritoAppService.DeletarNoticiaFavoritoById(idNoticiaFavorito);
            return Ok(response);
        }

        [HttpGet("visualizar/{idNoticiaFavorito:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visualizar(int idNoticiaFavorito)
        {
            var response = await _noticiaFavoritoAppService.VisualizarNoticiaFavoritoById(idNoticiaFavorito);
            return Ok(response);
        }
    }
}
