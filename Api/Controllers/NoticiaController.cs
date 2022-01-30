using Api.Config;
using Application.AppServices;
using Domain.Models;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("noticia")]
    public class NoticiaController : BaseController
    {
        private readonly NoticiaAppService _noticiaAppService;        
        private readonly MetricaAppService _metricaAppService;        

        public NoticiaController(NoticiaAppService noticiaAppService, MetricaAppService metricaAppService)
        {
            _noticiaAppService = noticiaAppService;
            _metricaAppService = metricaAppService;
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

        [ValidateUser]
        [HttpGet("listar-noticias")] //com a query melhorada
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarNoticias()
        {
            var usuario = ObterUsuario();
            var response = await _noticiaAppService.ListarNoticias(new NoticiaRequest() { IdUsuario = usuario.Id});
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
        public async Task<IActionResult> Visualizar(long idNoticia)        
        {            
            var response = await _noticiaAppService.VisualizarNoticiaById(idNoticia);
            return Ok(response);
        }

        [HttpPost("metrica-noticia")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ContabilizarClick(long idUsuario, long idNoticia)
        {
            await _metricaAppService.ContabilizarClick(idUsuario,idNoticia);
            var response = new ResponseViewModel()
            {
                Sucesso = true
            };
            return Ok(response);
        }
    }
}
