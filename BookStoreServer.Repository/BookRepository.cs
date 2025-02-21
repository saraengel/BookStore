using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            if (books.Count == 0) return null;            
            return _mapper.Map<List<BookDTO>>(books);
        }
        public BookDTO? GetBook(int id)
        {
            var book = _db.Books.SingleOrDefault(b => b.Id == id);
            if(book == null) return null;
            return _mapper.Map<BookDTO>(book);

        }
        public BookDTO AddBook(BaseEntityRequest<BookDTO> request)
        {
            var book = _mapper.Map<Book>(request.Entity);
            _db.Books.Add(book);
            _db.SaveChanges();
            return _mapper.Map<BookDTO>(book);

        }
        public BookDTO? UpdateBook(BaseEntityRequest<BookDTO> request)
        {
            Book? book = _db.Books.Find(request.Entity.Id);
            if (book == null) return null;
            _mapper.Map(request.Entity, book);
            _db.SaveChanges();
            return _mapper.Map<BookDTO>(book);
        }
        public BookDTO? UpdateBookPrice(BaseEntityRequest<BookDTO> request)
        {
            Book? book = _mapper.Map<Book>(GetBook(request.Entity.Id));
            if (book == null) return null;
            book.Price = book.Price;
            _db.SaveChanges();
            return _mapper.Map<BookDTO>(book);
        }
        public void DeleteBook(int id)
        {
            var book = _db.Books.SingleOrDefault(b=>b.Id==id);
            if (book == null) return;
            _db.Books.Remove(book);
            _db.SaveChanges();

        }
        public List<BookDTO> GetRangePriceOfBooks(RangePriceRequest request)
        {
            List<Book> books =  _db.Books.Where(b => b.Price > request.MinPrice && b.Price < request.MaxPrice).ToList();
            if (books.Count==0) return null;
            return _mapper.Map<List<BookDTO>>(books);
        }
        public async Task<List<BookDTO>> GetOldBooksAsync(DateTime date)
        {
            List<Book> books = await _db.Books.Where(b => b.PublishedDate < date).ToListAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}