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
    public class UserRepository: IUserRepository
    {
        private readonly BookStorContext _db;
        private IMapper _mapper;
        public UserRepository(BookStorContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public UserDTO GetUserByUserName(string userName)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserName == userName);
            if (user == null) { return null; }
            return _mapper.Map<UserDTO>(user);
        }
        public List<UserDTO> GetUsers(){
            List<User> users = _db.Users.ToList();
            return _mapper.Map<List<UserDTO>>(users);
        }
       
    }
}
