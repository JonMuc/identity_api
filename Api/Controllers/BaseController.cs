using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Util.Jwt;

namespace ApiCrud.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Usuario _usuario
        {
            get
            {
                var headerToken = Request.Headers["token"];
                return JWTManager.ValidateToken(headerToken);
            }
        }
    }
}
