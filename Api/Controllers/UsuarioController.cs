using Api.Config;
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
    [ApiController, Route("usuario")]
    public class UsuarioController : BaseController
    {
        private readonly UsuarioAppService _usuarioAppService;

        public UsuarioController(UsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpGet("buscar-usuario/{nomeUsuario}/{pageIndex}/{pageSize}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Buscar(string nomeUsuario, int pageIndex, int pageSize) {
            var response = await _usuarioAppService.BuscarUsuario(nomeUsuario, pageIndex, pageSize);
            return Ok(response);
        }

        [HttpPost("salvar-usuario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Salvar([FromBody] Usuario usuario)
        {
            var response = await _usuarioAppService.AdicionarUsuario(usuario);
            return Ok(response);
        }

        [ValidateUser]
        [HttpPost("atualizar-usuario")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] Usuario usuario)
        {
            usuario.Id = ObterUsuario().Id;
            var response = await _usuarioAppService.AtualizarUsuario(usuario);
            return Ok(response);
        }

        [HttpDelete("excluir-usuario/{idUsuario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deletar(int idUsuario)
        {
            var response = await _usuarioAppService.DeletarUsuarioById(idUsuario);
            return Ok(response);
        }

        [HttpGet("visualizar-usuario/{idUsuario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visualizar(int idUsuario)
        {
            var response = await _usuarioAppService.VisualizarUsuarioById(idUsuario);
            return Ok(response);
        }

        [HttpPost("criar-usuario-step")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SalvarUsuarioStep([FromBody] CriarContaUsuario usuario)
        {
            var response = await _usuarioAppService.CriarUsuarioStep(usuario);
            return Ok(response);
        }

        [HttpPost("enviar-foto/{id}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EnviarFotoAsync(long id)
        {
            UploadImagemRequest request = new UploadImagemRequest();
            var file = HttpContext.Request.Form.Files.Count > 0 ? HttpContext.Request.Form.Files[0] : null;
            request.IdUsuario = id;
            var data = new MemoryStream();
            file.CopyTo(data);
            request.FileStreamIO = data;
            var response = await _usuarioAppService.UploadImagemAsync(request);
            return Ok(response);
        }

        [ValidateUser]
        [HttpPost("editar-foto")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EditarFotoAsync()
        {
            UploadImagemRequest request = new UploadImagemRequest();
            var file = HttpContext.Request.Form.Files.Count > 0 ? HttpContext.Request.Form.Files[0] : null;
            request.IdUsuario = ObterUsuario().Id;
            var data = new MemoryStream();
            file.CopyTo(data);
            request.FileStreamIO = data;
            var response = await _usuarioAppService.UploadImagemAsync(request);
            return Ok(response);
        }

        [HttpPost("seguir-usuario/{idUsuarioSeguido}/{idUsuarioSeguidor}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SeguirUsuario(long idUsuarioSeguido, long idUsuarioSeguidor)
        {
            var response = await _usuarioAppService.SeguirUsuario(idUsuarioSeguido, idUsuarioSeguidor);
            return Ok(response);
        }

        [HttpPost("deseguir-usuario/{idUsuarioDeseguido}/{idUsuarioDeseguindo}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeseguirUsuario(long idUsuarioDeseguido, long idUsuarioDeseguindo)
        {
            var response = await _usuarioAppService.DeseguirUsuario(idUsuarioDeseguido, idUsuarioDeseguindo);
            return Ok(response);
        }

        [HttpGet("visualizar-seguidores/{idUsuario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> VisualizarSeguidores(int idUsuario)
        {
            var response = await _usuarioAppService.VisualizarSeguidores(idUsuario);
            return Ok(response);
        }

        [HttpGet("visualizar-seguindo/{idUsuario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> VisualizarSeguindo(int idUsuario)
        {
            var response = await _usuarioAppService.VisualizarSeguindo(idUsuario);
            return Ok(response);
        }

        [HttpGet("visualizar-perfil-usuario/{idUsuario:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> VisualizarPerfilUsuario(int idUsuario)
        {
            var response = await _usuarioAppService.VisualizarPerfilUsuario(idUsuario);
            return Ok(response);
        }

    }
}
