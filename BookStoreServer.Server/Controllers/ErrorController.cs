using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
                _logger = logger;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorOnDevelopment([FromServices] IHostEnvironment env)
        {
            LogError();
            var exeptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(
                detail: exeptionHandlerFeature?.Error?.StackTrace,
                title: exeptionHandlerFeature?.Error?.Message);
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        [Route("/error")]
        public IActionResult HandleError()
        {
            LogError();
            return Problem();
        }

        private void LogError()
        {
            var exeptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exeptionHandlerFeature != null)
            {
                _logger.LogError($"an unexpected error accured in EventMaster API {exeptionHandlerFeature?.Path}{exeptionHandlerFeature?.Error.ToString()}{exeptionHandlerFeature?.Error?.StackTrace}");

            }
        }
    }
}
