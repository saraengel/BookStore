using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(string userName, IEnumerable<Claim> additionalClaims = null);
    }
}
