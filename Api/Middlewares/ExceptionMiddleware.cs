using Domain.Models;
using Domain.Util;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ParametroException parametroException)
            {
                await ComportamentoParametroExceptionAsync(httpContext, parametroException);
            }
            catch (Exception exception)
            {
                await ComportamentoExceptionAsync(httpContext, exception);
            }
        }

        private Task ComportamentoParametroExceptionAsync(HttpContext context, ParametroException parametroException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseViewModel { Sucesso = false, Objeto = parametroException.Erros }));
        }

        private Task ComportamentoExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseViewModel { Sucesso = false, Objeto = new List<string> { { exception.Message } } }));
        }
    }
}
