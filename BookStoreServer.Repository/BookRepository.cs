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
    public class BookRepository : IBookRepository
    {
        private readonly BookStorContext _db;
        private readonly IMapper _mapper;

        public BookRepository(BookStorContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            var books = await _db.Books.ToListAsync();
            return books.Count == 0 ? null : _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetBookAsync(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(b => b.Id == id);
            return book == null ? null : _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> AddBookAsync(BaseEntityRequest<BookDTO> request)
        {
            var book = _mapper.Map<Book>(request.Entity);
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> UpdateBookAsync(BaseEntityRequest<BookDTO> request)
        {
            var book = await _db.Books.FindAsync(request.Entity.Id);
            if (book == null) return null;
            _mapper.Map(request.Entity, book);
            await _db.SaveChangesAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> UpdateBookPriceAsync(BaseEntityRequest<BookDTO> request)
        {
            var book = await _db.Books.FindAsync(request.Entity.Id);
            if (book == null) return null;
            book.Price = request.Entity.Price;
            await _db.SaveChangesAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (book == null) return;
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<List<BookDTO>> GetRangePriceOfBooksAsync(RangePriceRequest request)
        {
            var books = await _db.Books
                .Where(b => b.Price > request.MinPrice && b.Price < request.MaxPrice)
                .ToListAsync();
            return books.Count == 0 ? null : _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<List<BookDTO>> GetOldBooksAsync(DateTime date)
        {
            var books = await _db.Books.Where(b => b.PublishedDate < date).ToListAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<List<BookDTO>> GetLowStockBooksAsync(CancellationToken stoppingToken)
        {
            var books = await _db.Books.Where(b => b.Amount < 3).ToListAsync(stoppingToken);
            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}