using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Services.Interfaces;

namespace BookStoreServer.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookHub _bookHubService;

        public BookService(IBookRepository bookRepository, IBookHub bookHub)
        {
            _bookRepository = bookRepository;
            _bookHubService = bookHub;
        }

        public BaseGetListResponse<BookDTO> GetAllBooks()
        {
            List<BookDTO> books = _bookRepository.GetAllBooks();
            return new BaseGetListResponse<BookDTO>() { Entities = books };
        }
        public BaseGetEntityResponse<BookDTO> GetBook(int id)
        {
            BookDTO book = _bookRepository.GetBook(id);
            return new BaseGetEntityResponse<BookDTO>() { Entity = book };
        }
        public BaseGetEntityResponse<BookDTO> AddBook(BaseEntityRequest<BookDTO> request)
        {
            BookDTO book = _bookRepository.AddBook(request);
            return new BaseGetEntityResponse<BookDTO>() { Entity = book, Status = ResponseStatus.Created };
        }
        public BaseGetEntityResponse<BookDTO> UpdateBook(BaseEntityRequest<BookDTO> request)
        {
            BookDTO book = _bookRepository.UpdateBook(request);
            return new BaseGetEntityResponse<BookDTO> { Entity = book };
        }
        public BaseGetEntityResponse<BookDTO> UpdateBookPrice(BaseEntityRequest<BookDTO> request)
        {
            BookDTO book = _bookRepository.UpdateBookPrice(request);
            return new BaseGetEntityResponse<BookDTO> { Entity = book };
        }
        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }
        public BaseGetListResponse<BookDTO> GetRangePriceOfBooks(RangePriceRequest request)
        {
            List<BookDTO> books = _bookRepository.GetRangePriceOfBooks(request);
            return new BaseGetListResponse<BookDTO>() { Entities = books };
        }
    }
}
