using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCrud.Controllers
{
    [ApiController, Route("pessoaJuridica")]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly PessoaJuridicaAppService _pessoaJuridicaAppService;

        public PessoaJuridicaController(PessoaJuridicaAppService pessoaJuridicaAppService)
        {
            _pessoaJuridicaAppService = pessoaJuridicaAppService;
        }

        [HttpGet("listar-pessoa-juridica/")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ListarPessoaJuridica()
        {
            var response = _pessoaJuridicaAppService.ListarPessoaJuridica();
            return Ok(response);
        }


        [HttpPost("adicionar-pessoa-juridica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult AdicionarPessoaJuridica([FromBody] PessoaJuridica requestViewModel)
        {
            var responseViewModel = _pessoaJuridicaAppService.AdicionarPessoaJuridica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpPost("atualizar-pessoa-juridica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult AtualizarPessoaJuridica([FromBody] PessoaJuridica requestViewModel)
        {
            var responseViewModel = _pessoaJuridicaAppService.AtualizarPessoaJuridica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpPost("deletar-pessoa-juridica")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult DeletarPessoaJuridica([FromBody] PessoaJuridica requestViewModel)
        {
            var responseViewModel = _pessoaJuridicaAppService.DeletarPessoaJuridica(requestViewModel);
            return Ok(responseViewModel);
        }

        [HttpGet("buscar-pessoa-juridica/{idPessoa:long}")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult BuscarPessoaJuridica(long idPessoa)
        {
            var responseViewModel = _pessoaJuridicaAppService.BuscarPessoaJuridica(idPessoa);
            return Ok(responseViewModel);
        }
    }
}
