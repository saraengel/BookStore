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

namespace BookStoreServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseGetListResponse<UserDTO> GetUsers()
        {
            List<UserDTO> users = _userRepository.GetUsers();
            if (users == null || users.Count == 0)
                return new BaseGetListResponse<UserDTO>()
                {
                    ErrorMessage = "",
                    Status = ResponseStatus.NotSucceeded,
                    Succeeded = false
                };        
            return new BaseGetListResponse<UserDTO> {Entities= users};
        }
    }
}
