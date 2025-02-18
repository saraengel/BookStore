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
    public class BookRepository:IBookRepository
    {
        private readonly BookStorContext _db;
        private readonly IMapper _mapper;
        public BookRepository( BookStorContext db ,IMapper mapper)
        {
           _db = db;
           _mapper = mapper;
        }
        public List<BookDTO> GetAllBooks()
        {
            List<Book> books = _db.Books.ToList();
            
            return _mapper.Map<List<BookDTO>>(books);
        }
        public BookDTO? GetBook(int id)
        {
            var book = _db.Books.SingleOrDefault(b => b.id == id);
            return _mapper.Map<BookDTO>(book);

        }
        public BookDTO SetBook(BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            _db.Books.Add(book);
            _db.SaveChanges();
            //return bookDTO;
            return _mapper.Map<BookDTO>(book);

        }
        public BookDTO? UpdateBook(int id,BookDTO bookDTO)
        {

            Book? book = _db.Books.Find(id);
            if (book == null) return null;
            _mapper.Map(bookDTO, book);
            _db.SaveChanges();
            return _mapper.Map<BookDTO>(book);
        }
        public BookDTO? UpdateBookPrice(BookDTO bookDTO)
        {
            Book? book = _mapper.Map<Book>(GetBook(bookDTO.id));
            if (book == null) return null;
            book.price = book.price;
            _db.SaveChanges();
            return bookDTO;
        }
        public void DeleteBook(int id)
        {
            var book = _db.Books.SingleOrDefault(b=>b.id==id);
            if (book == null) return;
            _db.Books.Remove(book);
            _db.SaveChanges();

        }
        public List<BookDTO>? GetRangePriceOfBooks(decimal minPrice, decimal MaxPrice)
        {
            List<Book> books =  _db.Books.Where(b => b.price > minPrice && b.price < MaxPrice).ToList();
             return _mapper.Map<List<BookDTO>>(books);
        }
        public async Task<List<BookDTO>> GetOldBooksAsync(DateTime date)
        {
            List<Book> books = await _db.Books.Where(b => b.PublishedDate < date).ToListAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}