using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService ;
        public AuthController(IAuthService authService)
        {
                _authService = authService ;
        }
        [HttpPost]
        [Route("/login")]
        public ActionResult Login(string username, string password)
        {
            string token = _authService.Login(username, password);
            return Ok(new {Token = token});
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
