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
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(
                detail: exceptionHandlerFeature?.Error?.StackTrace,
                title: exceptionHandlerFeature?.Error?.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError()
        {
            LogError();
            return Problem();
        }

        private void LogError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerFeature != null)
            {
                _logger.LogError($"An unexpected error occurred in BookStore API: {exceptionHandlerFeature.Path} {exceptionHandlerFeature.Error} {exceptionHandlerFeature.Error?.StackTrace}");
            }
        }
    }
}
