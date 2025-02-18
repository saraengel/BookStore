using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;

namespace BookStoreServer.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetUsers();
    }
}
