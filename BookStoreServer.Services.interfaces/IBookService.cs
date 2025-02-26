using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Services.Interfaces
{
    public interface  IBookService
    {
        public BaseGetListResponse<BookDTO> GetAllBooks();
        public BaseGetEntityResponse<BookDTO> GetBook(int id);
        public BaseGetEntityResponse<BookDTO> AddBook(BaseEntityRequest<BookDTO> request);
        public BaseGetEntityResponse<BookDTO> UpdateBook(BaseEntityRequest<BookDTO> request);
        public BaseGetEntityResponse<BookDTO> UpdateBookAmount(BaseEntityRequest<BookDTO> request);
        public void DeleteBook(int id);
        public BaseGetListResponse<BookDTO> GetRangePriceOfBooks(RangePriceRequest request);
    }
}
