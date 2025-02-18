using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Services.Interfaces;

namespace BookStoreServer.Service
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

        public List<BookDTO> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
        public BookDTO? GetBook(int id)
        {
            return _bookRepository.GetBook(id);

        }
        public BookDTO SetBook(BookDTO book)
        {
            return _bookRepository.SetBook(book);

        }
        public BookDTO? UpdateBook(int id, BookDTO book)
        {
            return _bookRepository.UpdateBook(id, book);
        }
        public BookDTO? UpdateBookPrice(BookDTO book)
        {
           
            return _bookRepository.UpdateBookPrice(book);
        }
        public void DeleteBook(int id)
        {
           _bookRepository.DeleteBook(id);

        }
        public List<BookDTO>? GetRangePriceOfBooks(decimal minPrice, decimal MaxPrice)
        {
            return _bookRepository.GetRangePriceOfBooks(minPrice, MaxPrice);
        }
    }
}
