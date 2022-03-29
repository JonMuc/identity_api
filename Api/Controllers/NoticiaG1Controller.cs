using Api.Config;
using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiCrud.Controllers
{
    [ApiController, Route("noticiaG1")]
    public class NoticiaG1Controller : BaseController
    {
        private readonly NoticiaAppService _informacaoGoogleAppService;

        public NoticiaG1Controller(NoticiaAppService informacaoGoogleAppService)
        {
            _informacaoGoogleAppService = informacaoGoogleAppService;
        }

        [HttpPost("listar-manchete")]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListarManchete([FromBody] DataRequest request)
        {
            var response = await _informacaoGoogleAppService.ListarMancheteG1(request);
            return Ok(response);
        }
    }
}
