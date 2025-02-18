using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Service;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
                _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserDTO> users = _userService.GetUsers();
            return Ok(users);
        }
    }
}
