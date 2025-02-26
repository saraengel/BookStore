using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("/login")]
        public ActionResult Login(LoginRequest request)
        {
            string token = _authService.Login(request);
            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("/logout")]
        public ActionResult Logout()
        {
            _authService.Logout();
            return Ok();
        }
    }
}
