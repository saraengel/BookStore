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
    /// <summary>
    /// AutoMapper profile for mapping between domain models and DTOs.
    /// </summary>
    public class BookProfileDal : Profile
    {
        public BookProfileDal()
        {
            // Mapping between Book and BookDTO
            CreateMap<Book, BookDTO>().ReverseMap();

            // Mapping between User and UserDTO
            CreateMap<User, UserDTO>().ReverseMap();

            // Mapping between Order and OrderDTO
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
