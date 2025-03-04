﻿using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<BaseGetListResponse<UserDTO>> GetUsers()
        {
           return _userService.GetUsers();         
        }
        [HttpPost]
        public BaseGetEntityResponse<UserDTO> AddAsync([FromBody] BaseEntityRequest<UserDTO> request)
        {
            return _userService.AddUser(request);
        }
    }
}
