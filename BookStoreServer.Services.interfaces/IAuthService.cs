using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.Request;

namespace BookStoreServer.Services.Interfaces
{
    public interface IAuthService
    {
        public string Login(LoginRequest request);
        public void Logout();
    }
}
