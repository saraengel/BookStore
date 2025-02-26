using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Entities.Events;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStorContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(BookStorContext db, IMapper mapper, ILogger<OrderRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            try
            {
                var orders = await _db.Orders.ToListAsync();
                return _mapper.Map<List<OrderDTO>>(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all orders");
                throw;
            }
        }

        public async Task<AddOrderRsponse> AddOrderAsync(OrderRequest request)
        {
            try
            {

                var bookAndUserOrder = await _db.Books.Join(_db.Users, b => b.Title == request.BookTitle && b.Amount >= request.Amount, u => u.Id == request.UserId, (book, user) => new { book, user })
                    .SingleOrDefaultAsync();


                if (bookAndUserOrder == null)
                {
                    throw new InvalidOperationException("Book not found or insufficient amount");
                }

                var order = new Order()
                {
                    BookId = bookAndUserOrder.book.Id,
                    UserId = bookAndUserOrder.user.Id,
                    Amount = request.Amount,
                    Status = OrderStatus.Pending,
                    IsValid = true
                };

                bookAndUserOrder.book.Amount -= request.Amount;

                await _db.Orders.AddAsync(order);
                _db.Books.Update(bookAndUserOrder.book);
                await _db.SaveChangesAsync();
                var orderCreatedEvent = new OrderCreatedEvent(request.UserId,bookAndUserOrder.user.UserName,bookAndUserOrder.user.Email, bookAndUserOrder.book.Title);
                var orderDTO = _mapper.Map<OrderDTO>(order);
                return new AddOrderRsponse() { orderCreatedEvent = orderCreatedEvent,order = orderDTO};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order");
                throw;
            }
        }

        public async Task UpdateStatusAsync(int orderId, OrderStatus status)
        {
            try
            {
                var order = await _db.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found: {OrderId}", orderId);
                    return;
                }

                order.Status = status;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order status");
                throw;
            }
        }

        public async Task SetValidationAsync(int orderId, bool isValid)
        {
            try
            {
                var order = await _db.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found: {OrderId}", orderId);
                    return;
                }

                order.IsValid = isValid;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting order validation");
                throw;
            }
        }
    }
}
