using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCrud.Controllers
{
    [ApiController, Route("noticiaGoogle")]
    public class NoticiaGoogleController : ControllerBase
    {
        private readonly NoticiaAppService _informacaoGoogleAppService;

        public NoticiaGoogleController(NoticiaAppService informacaoGoogleAppService)
        {
            _informacaoGoogleAppService = informacaoGoogleAppService;
        }

        [HttpGet("listar-manchete")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ListarManchete()
        {
            var response = _informacaoGoogleAppService.ListarManchete();
            return Ok(response);
        }


        //[HttpPost("adicionar-pessoa-fisica")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public IActionResult AdicionarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        //{
        //    var responseViewModel = _pessoaFisicaAppService.AdicionarPessoaFisica(requestViewModel);
        //    return Ok(responseViewModel);
        //}

        //[HttpPost("atualizar-pessoa-fisica")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public IActionResult AtualizarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        //{
        //    var responseViewModel = _pessoaFisicaAppService.AtualizarPessoaFisica(requestViewModel);
        //    return Ok(responseViewModel);
        //}

        //[HttpPost("deletar-pessoa-fisica")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public IActionResult DeletarPessoaFisica([FromBody] PessoaFisica requestViewModel)
        //{
        //    var responseViewModel = _pessoaFisicaAppService.DeletarPessoaFisica(requestViewModel);
        //    return Ok(responseViewModel);
        //}

        //[HttpGet("buscar-pessoa-fisica/{idPessoa:long}")]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        //public IActionResult BuscarPessoaFisica(long idPessoa)
        //{
        //    var responseViewModel = _pessoaFisicaAppService.BuscarPessoaFisica(idPessoa);
        //    return Ok(responseViewModel);
        //}
    }
}
