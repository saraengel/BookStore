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
        public Task<BaseGetListResponse<BookDTO>> GetAllBooksAsync();
        public  Task<BaseGetEntityResponse<BookDTO>> GetBookAsync(int id);
        public Task<BaseGetEntityResponse<BookDTO>> AddBookAsync(BaseEntityRequest<BookDTO> request);
        public Task<BaseGetEntityResponse<BookDTO>> UpdateBookAsync(BaseEntityRequest<BookDTO> request);
        public Task<BaseGetEntityResponse<BookDTO>> UpdateBookPriceAsync(BaseEntityRequest<BookDTO> request);
        public Task DeleteBookAsync(int id);
        public Task<BaseGetListResponse<BookDTO>> GetRangePriceOfBooksAsync(RangePriceRequest request);
    }
}
