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
        public UserDTO GetUserByUserName(string userName);
        public List<UserDTO> GetUsers();
    }
}
