using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;

namespace BookStoreServer.Services.Interfaces
{
    public interface  IBookService
    {
        public List<BookDTO> GetAllBooks();
        public BookDTO? GetBook(int id);
        public BookDTO SetBook(BookDTO book);
        public BookDTO? UpdateBook(int id, BookDTO book);
        public BookDTO? UpdateBookPrice(BookDTO book);
        public void DeleteBook(int id);
        public List<BookDTO>? GetRangePriceOfBooks(decimal minPrice, decimal MaxPrice);
    }
}
