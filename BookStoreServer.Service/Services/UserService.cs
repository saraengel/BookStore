using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseGetListResponse<UserDTO>> GetUsersAsync()
        {
            try
            {
                List<UserDTO> users = await _userRepository.GetUsersAsync();
                if (users == null || users.Count == 0)
                {
                    return new BaseGetListResponse<UserDTO>
                    {
                        ErrorMessage = "No users found.",
                        Status = ResponseStatus.NotSucceeded,
                        Succeeded = false
                    };
                }

                return new BaseGetListResponse<UserDTO>
                {
                    Entities = users,
                    TotalCount = users.Count,
                    Succeeded = true,
                    Status = ResponseStatus.Succeeded
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting users.");
                return new BaseGetListResponse<UserDTO>
                {
                    ErrorMessage = "An error occurred while getting users.",
                    Status = ResponseStatus.Failed,
                    Succeeded = false
                };
            }
        }
    }
}
