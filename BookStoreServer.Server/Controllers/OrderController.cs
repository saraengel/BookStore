using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Service.Services;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;  
        }

        [HttpGet]
        public ActionResult<BaseGetListResponse<OrderDTO>> Get()
        {
            return _orderService.GetAll();
           
        }
        [HttpPost]
        public ActionResult<BaseResponse> Set([FromBody] OrderRequest request)
        {
            _orderService.AddOrder(request);
            return Ok();
        }
        [HttpPost("ProcessOrdersAsync") ]
        public async Task<IActionResult> ProcessOrdersAsync()
        {
           await _orderService.ProcessOrdersAsync();
            return Ok();
        }
        
    }
}
