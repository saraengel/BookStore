using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Models;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Entities;
using BookStoreServer.Entities.AppSettings;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Service.Cache;
using BookStoreServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreServer.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _JWTService;
        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _JWTService = jwtService;
        }
        public string Login(LoginRequest request)
        {
            var user = GetUser(request.Username);
            if (user == null) { return null; }
            //check validation
            return _JWTService.GenerateToken(request.Username);
            //CreateSession(user, userName);
        }

        public void Logout()
        {
            //_sessionMemoryCache.Delete()
            throw new NotImplementedException();
        }
        public UserDTO GetUser(string username)
        {
            return _userRepository.GetUserByUserName(username);
        }
        //public void CreateSession(UserDTO user, string userName) {  
        //     string sessionId = Guid.NewGuid().ToString();
        //     SessionData session = new SessionData() { UserIdentity = user.Id, RoleId = user.RoleId, UserType = UserType.Site, RunMockTransaction = (bool)user.RunMockTransactions };
        //     _sessionMemoryCache.Add(sessionId, session);
        //}
    }
}
