using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetIdentityModel.Models;
using System.Diagnostics;

namespace NetIdentityModel.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        { 
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //// Retorna detalhes da exceção
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }

    }
}