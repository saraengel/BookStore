using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;

        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<BookDTO> books = _bookService.GetAllBooks();
            _logger.LogError("GetAllBooks");
                return Ok(books);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetBook([FromRoute] int id)
        {
            BookDTO? book = _bookService.GetBook(id);
            return Ok(book);
        }
        [Authorize]
        [HttpPost("setBook")]
        public IActionResult SetBook([FromBody] BookDTO book)
        {
            BookDTO newBook = _bookService.SetBook(book);
            return Created("api/books" + newBook.id, newBook);
        }
        [Authorize]
        [HttpPut("updateBook/{id}")]
        public IActionResult UpdateBook([FromBody] BookDTO book, [FromRoute] int id)
        {
            BookDTO? newBook = _bookService.UpdateBook(id, book);
            return Ok(newBook);
        }
        [Authorize]
        [HttpPatch("updateBookPrice/{id}")]
        public IActionResult UpdateBookPrice([FromBody] BookDTO book, [FromRoute] int id)
        {
            book.id = id;
            BookDTO? newBook = _bookService.UpdateBookPrice(book);
            return Ok(newBook);
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
        [Authorize]
        [HttpGet("price-range")]
        public IActionResult getRangePriceOfBooks([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            List<BookDTO>? books = _bookService.GetRangePriceOfBooks(minPrice, maxPrice);
            return Ok(books);
        }
    }
}
