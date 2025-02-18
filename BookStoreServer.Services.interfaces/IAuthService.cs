using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Services.Interfaces
{
    public interface IAuthService
    {
        public string Login(string username, string password);
        public void Logout();
    }
}
