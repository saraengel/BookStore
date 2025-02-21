using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookStoreServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<BaseGetListResponse<BookDTO>> GetAll()
        {
            return _bookService.GetAllBooks();
        }
        //[Authorize]
        [HttpGet("{id}")]
        public  ActionResult<BaseGetEntityResponse<BookDTO>> Get([FromRoute] int id)
        {
            return _bookService.GetBook(id);
        }
        //[Authorize]
        [HttpPost]
        public ActionResult<BaseGetEntityResponse<BookDTO>> Set([FromBody] BaseEntityRequest<BookDTO> book)
        {
            return _bookService.AddBook(book);
            //await _bookHub.Clients.All.SendAsync("ReceiveBookNotification", $"📖 ספר חדש נוסף: {book.title}");
        }
        //[Authorize]
        [HttpPut]
        public ActionResult<BaseGetEntityResponse<BookDTO>> Update([FromBody]BaseEntityRequest<BookDTO> request)
        {
            return _bookService.UpdateBook( request);
        }
        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
        //[Authorize]
        [HttpPatch("updateBookPrice/{id}")]
        public ActionResult<BaseGetEntityResponse<BookDTO>> UpdateBookPrice([FromBody] BaseEntityRequest<BookDTO> request)
        {
            return _bookService.UpdateBookPrice(request);
        }
        //[Authorize]
        [HttpGet("price-range")]
        public ActionResult<BaseGetListResponse<BookDTO>> getRangePriceOfBooks([FromQuery] RangePriceRequest request)
        {
            return _bookService.GetRangePriceOfBooks(request);
        }
    }
}
