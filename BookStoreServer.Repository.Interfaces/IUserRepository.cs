using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;

namespace BookStoreServer.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDTO> GetUserByUserNameAsync(string userName);
        public Task<List<UserDTO>> GetUsersAsync();
    }
}
