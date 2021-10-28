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

        [HttpPost("salvar")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SalvarAsync([FromBody] Comentario request)
        {
            var response = await _comentarioAppService.AdicionarAsync(request);
            return Ok(response);
        }

        //[HttpPost("atualizar-usuario")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> Atualizar([FromBody] Usuario usuario)
        //{
        //    var response = await _usuarioAppService.AtualizarUsuario(usuario);
        //    return Ok(response);
        //}

        //[HttpDelete("excluir-usuario/{idUsuario:int}")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> Deletar(int idUsuario)
        //{
        //    var response = await _usuarioAppService.DeletarUsuarioById(idUsuario);
        //    return Ok(response);
        //}

        //[HttpGet("visualizar-usuario/{idUsuario:int}")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> Visualizar(int idUsuario)
        //{
        //    var response = await _usuarioAppService.VisualizarUsuarioById(idUsuario);
        //    return Ok(response);
        //}
    }
}
