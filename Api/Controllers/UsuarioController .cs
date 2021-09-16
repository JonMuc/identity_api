using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCrud.Controllers
{
    [ApiController, Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly PessoaFisicaAppService _pessoaFisicaAppService;

        public UsuarioController(PessoaFisicaAppService pessoaFisicaAppService)
        {
            _pessoaFisicaAppService = pessoaFisicaAppService;
        }

        [HttpGet("listar-pessoa-fisica/")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ListarPessoaFisica()
        {
            var response = _pessoaFisicaAppService.ListarPessoaFisica();
            return Ok(response);
        }


        [HttpPost("adicionar-pessoa-fisica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult AdicionarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        {
            var responseViewModel = _pessoaFisicaAppService.AdicionarPessoaFisica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpPost("atualizar-pessoa-fisica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult AtualizarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        {
            var responseViewModel = _pessoaFisicaAppService.AtualizarPessoaFisica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpPost("deletar-pessoa-fisica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult DeletarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        {
            var responseViewModel = _pessoaFisicaAppService.DeletarPessoaFisica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpGet("buscar-pessoa-fisica/{idPessoa:long}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult BuscarPessoaFisica(long idPessoa)
        {
            var responseViewModel = _pessoaFisicaAppService.BuscarPessoaFisica(idPessoa);
            return Ok(responseViewModel);
        }
    }
}
