using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Response;
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
        public BaseGetListResponse<UserDTO> GetUsers()
        {
           return _userService.GetUsers();
           
        }
    }
}
