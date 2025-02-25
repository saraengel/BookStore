using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Entities.AppSettings;
using BookStoreServer.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreServer.Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly JWTSettings _JWTSettings;

        public JwtService(IOptions<JWTSettings> jwtSettings)
        {
            _JWTSettings = jwtSettings.Value;
        }

        public string GenerateToken(string userName, IEnumerable<Claim> additionalClaims = null)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_JWTSettings.Key);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userName)
                    };

                if (additionalClaims != null)
                {
                    claims.AddRange(additionalClaims);
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(_JWTSettings.TokenExpirationInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("Error generating JWT token", ex);
            }
        }
    }
}
