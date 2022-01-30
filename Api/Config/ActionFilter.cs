using ApiCrud.Controllers;
using Domain.Models;
using Domain.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Util.Jwt;

namespace Api.Config
{
    public class ValidateUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var errosResponse = new List<string>(0);
            errosResponse.Add("Token Inválido!");
            var headerToken = filterContext.HttpContext.Request.Headers["token"];
            var tokenUser = JWTManager.ValidateToken(headerToken);
            if (tokenUser == null || tokenUser.Id == 0 )
                throw new ParametroException(errosResponse);
        }
    }
}
