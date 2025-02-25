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
using BookStoreServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreServer.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _JWTService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _JWTService = jwtService;
            _logger = logger;
        }

        public async Task<string> Login(LoginRequest request)
        {
            try
            {
                var user = await GetUser(request.Username);
                if (user == null || !ValidatePassword(request.Password, user.Password))
                {
                    _logger.LogWarning("Invalid login attempt for user: {Username}", request.Username);
                    return null;
                }

                _logger.LogInformation("User {Username} logged in successfully", request.Username);
                return _JWTService.GenerateToken(request.Username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user: {Username}", request.Username);
                return ex.ToString();
            }
        }

        public void Logout()
        {
            // Invalidate the JWT token by adding it to a blacklist or changing the user's token version
            // This is a placeholder implementation and should be replaced with actual logic
            _logger.LogInformation("User logged out successfully");
        }

        private async Task<UserDTO> GetUser(string username)
        {
            return await _userRepository.GetUserByUserNameAsync(username);
        }

        private bool ValidatePassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword; 
        }
    }
}
