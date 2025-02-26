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

        public List<OrderDTO> GetAll()
        {
            try
            {
                var orders = _db.Orders.ToList();
                return _mapper.Map<List<OrderDTO>>(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all orders");
                throw;
            }
        }

        public AddOrderRsponse AddOrder(OrderRequest request)
        {
            try
            {

                var bookAndUserOrder = _db.Books.Join(_db.Users, b => b.Title == request.BookTitle && b.Amount >= request.Amount, u => u.Id == request.UserId, (book, user) => new { book, user })
                    .SingleOrDefault();


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

                _db.Orders.Add(order);
                _db.Books.Update(bookAndUserOrder.book);
                _db.SaveChanges();
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

        public void UpdateStatus(int orderId, OrderStatus status)
        {
            try
            {
                var order = _db.Orders.Find(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found: {OrderId}", orderId);
                    return;
                }

                order.Status = status;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order status");
                throw;
            }
        }

        public void SetValidation(int orderId, bool isValid)
        {
            try
            {
                var order = _db.Orders.Find(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found: {OrderId}", orderId);
                    return;
                }

                order.IsValid = isValid;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting order validation");
                throw;
            }
        }
    }
}
