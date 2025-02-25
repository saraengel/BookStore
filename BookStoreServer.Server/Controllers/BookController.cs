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
        public async Task<ActionResult<BaseGetListResponse<BookDTO>>> GetAllAsync()
        {
            return await _bookService.GetAllBooksAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseGetEntityResponse<BookDTO>>> GetAsync([FromRoute] int id)
        {
            return await _bookService.GetBookAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<BaseGetEntityResponse<BookDTO>>> AddAsync([FromBody] BaseEntityRequest<BookDTO> book)
        {
            return await _bookService.AddBookAsync(book);
        }

        [HttpPut]
        public async Task<ActionResult<BaseGetEntityResponse<BookDTO>>> UpdateAsync([FromBody] BaseEntityRequest<BookDTO> request)
        {
            return await _bookService.UpdateBookAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPatch("updateBookPrice/{id}")]
        public async Task<ActionResult<BaseGetEntityResponse<BookDTO>>> UpdateBookPriceAsync([FromBody] BaseEntityRequest<BookDTO> request)
        {
            return await _bookService.UpdateBookPriceAsync(request);        
        }

        [HttpGet("price-range")]
        public async Task<ActionResult<BaseGetListResponse<BookDTO>>> GetRangePriceOfBooksAsync([FromQuery] RangePriceRequest request)
        {
            return await _bookService.GetRangePriceOfBooksAsync(request);
        }
    }
}
