using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Model.Models;

namespace BookStoreServer.Repository.AutoMapper
{
    public class BookProfileDal:Profile
    {
        public BookProfileDal()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
