using Application.AppServices;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("perfil-usuario")]
    public class PerfilUsuarioController : ControllerBase
    {
        private readonly PerfilUsuarioAppService _perfilUsuarioAppService;

        public PerfilUsuarioController(PerfilUsuarioAppService perfilUsuarioAppService)
        {
            _perfilUsuarioAppService = perfilUsuarioAppService;
        }

        [HttpPost("salvar-perfil")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Salvar(PerfilUsuarioRequest perfilUsuario)
        {
            var response = await _perfilUsuarioAppService.AdicionarPerfil(perfilUsuario);
            return Ok(response);
        }

        [HttpDelete("excluir-perfil")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deletar(long IdPerfil)
        {
            var response = await _perfilUsuarioAppService.DeletarPerfilUsuarioById(IdPerfil);
            return Ok(response);
        }

        [HttpGet("visualizar-perfil")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visualizar(long idUsuario)
        {
            var response = await _perfilUsuarioAppService.VisualizarPerfilUsuarioCompleto(idUsuario);
            return Ok(response);
        }
    }
}
