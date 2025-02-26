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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<BaseGetListResponse<BookDTO>> GetAll()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public ActionResult<BaseGetEntityResponse<BookDTO>> Get([FromRoute] int id)
        {
            return _bookService.GetBook(id);
        }

        [HttpPost]
        public ActionResult<BaseGetEntityResponse<BookDTO>> Add([FromBody] BaseEntityRequest<BookDTO> book)
        {
            return _bookService.AddBook(book);
        }

        [HttpPut]
        public ActionResult<BaseGetEntityResponse<BookDTO>> Update([FromBody] BaseEntityRequest<BookDTO> request)
        {
            return _bookService.UpdateBook(request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpPatch("updateBookPrice/{id}")]
        public ActionResult<BaseGetEntityResponse<BookDTO>> UpdateBookAmount([FromBody] BaseEntityRequest<BookDTO> request)
        {
            return _bookService.UpdateBookAmount(request);        
        }

        [HttpGet("price-range")]
        public ActionResult<BaseGetListResponse<BookDTO>> GetRangePriceOfBooks([FromQuery] RangePriceRequest request)
        {
            return _bookService.GetRangePriceOfBooks(request);
        }
    }
}
