using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;

namespace BookStoreServer.Repository.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookDTO>> GetAllBooksAsync();
        public Task<BookDTO?> GetBookAsync(int id);
        public Task<BookDTO> AddBookAsync(BaseEntityRequest<BookDTO> request);
        public Task<BookDTO?> UpdateBookAsync(BaseEntityRequest<BookDTO> request);
        public Task<BookDTO?> UpdateBookPriceAsync(BaseEntityRequest<BookDTO> request);
        public Task DeleteBookAsync(int id);
        public Task<List<BookDTO>> GetRangePriceOfBooksAsync(RangePriceRequest request);
        public Task<List<BookDTO>> GetOldBooksAsync(DateTime date);
        public Task<List<BookDTO>> GetLowStockBooksAsync(CancellationToken stoppingToken);

    }
}
