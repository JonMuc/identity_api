using Application.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrud.Controllers
{
    [ApiController, Route("avaliacao")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly ComentarioAppService _comentarioAppService;

        public AvaliacaoController(ComentarioAppService comentarioAppService)
        {
            _comentarioAppService = comentarioAppService;
        }


    }
}
