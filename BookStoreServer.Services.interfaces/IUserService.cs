using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Response;

namespace BookStoreServer.Services.Interfaces
{
    public interface IUserService
    {
        public BaseGetListResponse<UserDTO> GetUsers();
    }
}
