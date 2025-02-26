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
        public List<BookDTO> GetAllBooks();
        public BookDTO? GetBook(int id);
        public BookDTO AddBook(BaseEntityRequest<BookDTO> request);
        public BookDTO? UpdateBook(BaseEntityRequest<BookDTO> request);
        public BookDTO? UpdateBookAmount(BaseEntityRequest<BookDTO> request);
        public void DeleteBook(int id);
        public List<BookDTO> GetRangePriceOfBooks(RangePriceRequest request);
        public List<BookDTO> GetOldBooks(DateTime date);
        public Task<List<BookDTO>> GetLowStockBooksAsync(CancellationToken stoppingToken);

    }
}
