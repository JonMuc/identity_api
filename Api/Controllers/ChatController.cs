using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("chat")]
    public class ChatController : ControllerBase
    {
        private readonly ChatAppService _chatAppService;

        public ChatController(ChatAppService chatAppService)
        {
            _chatAppService = chatAppService;
        }               

        [HttpPost("incluir")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Salvar([FromBody] Chat chat)
        {
            var response = await _chatAppService.AdicionarChat(chat);
            return Ok(response);
        }

        [HttpDelete("excluir/{idChat:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Deletar(int idChat)
        {
            var response = await _chatAppService.DeletarChatById(idChat);
            return Ok(response);
        }

        [HttpPost("atualizar")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] Chat chat)
        {
            var response = await _chatAppService.AtualizarChat(chat);
            return Ok(response);
        }


        [HttpGet("visualizar/{idChat:int}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visualizar(int idChat)
        {
            var response = await _chatAppService.VisualizarChatById(idChat);
            return Ok(response);
        }

        [HttpGet("listar-mensagens")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarMensagens(long idUsuarioEnvio, long idUsuarioRecebe)
        {
            var response = await _chatAppService.ListarMensagens(idUsuarioEnvio, idUsuarioRecebe);
            return Ok(response);
        }

    }
}
