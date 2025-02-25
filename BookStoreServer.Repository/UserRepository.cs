using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStorContext _db;
        private readonly IMapper _mapper;

        public UserRepository(BookStorContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByUserNameAsync(string userName)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null) { return null; }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _db.Users.ToListAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}
