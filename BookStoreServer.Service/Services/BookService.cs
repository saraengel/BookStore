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

        public async Task<BaseGetListResponse<BookDTO>> GetAllBooksAsync()
        {
            try
            {
                List<BookDTO> books = await _bookRepository.GetAllBooksAsync();
                return new BaseGetListResponse<BookDTO> { Entities = books };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all books.");
                return new BaseGetListResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<BaseGetEntityResponse<BookDTO>> GetBookAsync(int id)
        {
            try
            {
                BookDTO book = await _bookRepository.GetBookAsync(id);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting book with id {id}.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<BaseGetEntityResponse<BookDTO>> AddBookAsync(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                ValidateBook(request.Entity);
                BookDTO book = await _bookRepository.AddBookAsync(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book, Status = ResponseStatus.Created };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a book.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<BaseGetEntityResponse<BookDTO>> UpdateBookAsync(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                ValidateBook(request.Entity);
                BookDTO book = await _bookRepository.UpdateBookAsync(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a book.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<BaseGetEntityResponse<BookDTO>> UpdateBookPriceAsync(BaseEntityRequest<BookDTO> request)
        {
            try
            {
                if (request.Entity.Price <= 0)
                {
                    throw new ArgumentException("Price must be greater than 0.");
                }
                BookDTO book = await _bookRepository.UpdateBookPriceAsync(request);
                return new BaseGetEntityResponse<BookDTO> { Entity = book };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating book price.");
                return new BaseGetEntityResponse<BookDTO> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            try
            {
                await _bookRepository.DeleteBookAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting book with id {id}.");
                throw;
            }
        }

        public async Task<BaseGetListResponse<BookDTO>> GetRangePriceOfBooksAsync(RangePriceRequest request)
        {
            try
            {
                List<BookDTO> books = await _bookRepository.GetRangePriceOfBooksAsync(request);
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
