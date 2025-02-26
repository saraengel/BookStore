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
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public BaseGetListResponse<BookDTO> GetAllBooks()
        {
            try
            {
                List<BookDTO> books = _bookRepository.GetAllBooks();
                return new BaseGetListResponse<BookDTO> { Entities = books };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all books.");
                return new BaseGetListResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public BaseGetEntityResponse<BookDTO> GetBook(int id)
        {
            try
            {
                BookDTO book = _bookRepository.GetBook(id);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting book with id {id}.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public BaseGetEntityResponse<BookDTO> AddBook(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                ValidateBook(request.Entity);
                BookDTO book =_bookRepository.AddBook(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book, Status = ResponseStatus.Created };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a book.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public BaseGetEntityResponse<BookDTO> UpdateBook(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                ValidateBook(request.Entity);
                BookDTO book = _bookRepository.UpdateBook(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a book.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public BaseGetEntityResponse<BookDTO> UpdateBookAmount(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                if (request.Entity.Amount <= 0)
                {
                    throw new ArgumentException("Amount must be greater than 0.");
                }
                BookDTO book = _bookRepository.UpdateBookAmount(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating book price.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public void DeleteBook(int id)
        {
            try
            {
                _bookRepository.DeleteBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting book with id {id}.");
                throw;
            }
        }

        public BaseGetListResponse<BookDTO> GetRangePriceOfBooks(RangePriceRequest request)
        {
            try
            {
                List<BookDTO> books = _bookRepository.GetRangePriceOfBooks(request);
                return new BaseGetListResponse<BookDTO> { Entities = books };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting books in price range.");
                return new BaseGetListResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        private void ValidateBook(BookDTO book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Title is required.");
            }
            if (book.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than 0.");
            }
            if (book.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0.");
            }
        }
    }
}
