using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Hubs;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookStoreServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IHubContext<BookHub> _bookHub;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, IHubContext<BookHub> bookHub, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _bookHub = bookHub;
            _logger = logger;

        }
        [Authorize]
        [HttpGet]
        public ActionResult<BaseResponse> GetAll()
        {
            List<BookDTO> books = _bookService.GetAllBooks();
            _logger.LogError("GetAllBooks");
            return Ok(books);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            BookDTO? book = _bookService.GetBook(id);
            return Ok(book);
        }
        [Authorize]
        [HttpPost("AddBook")]
        public async Task<IActionResult> Set([FromBody] BookDTO book)
        {
            BookDTO newBook = _bookService.AddBook(book);
            await _bookHub.Clients.All.SendAsync("ReceiveBookNotification", $"📖 ספר חדש נוסף: {book.title}");
            return Created("api/books" + newBook.id, newBook);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] BookDTO book, [FromRoute] int id)
        {
            BookDTO? newBook = _bookService.UpdateBook(id, book);
            return Ok(newBook);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
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
        [HttpGet("price-range")]
        public IActionResult getRangePriceOfBooks([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            List<BookDTO>? books = _bookService.GetRangePriceOfBooks(minPrice, maxPrice);
            return Ok(books);
        }
    }
}
