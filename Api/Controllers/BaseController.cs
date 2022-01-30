using Api.Config;
using Application.AppServices;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Util.Jwt;

namespace ApiCrud.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Usuario ObterUsuario()
        {
            var headerToken = Request.Headers["token"];
            return JWTManager.ValidateToken(headerToken);
        }
    }
}
