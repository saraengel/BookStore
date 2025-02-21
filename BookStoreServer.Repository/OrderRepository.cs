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
using BookStoreServer.Entities;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository.Interfaces;

namespace BookStoreServer.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly BookStorContext _db;
        private IMapper _mapper;
        public OrderRepository(BookStorContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<OrderDTO> GetAll()
        {
            return _mapper.Map<List<OrderDTO>>(_db.Orders);
        }
        public OrderDTO AddOrder(OrderRequest request)
        {
            Book book = _db.Books.Where(b => b.Title == request.BookTitle).SingleOrDefault();
            Order order = new Order() { 
                BookId = book.Id,
                UserId = request.UserId,
                Amount = request.Amount
            };
             _db.Orders.Add(order);
             _db.SaveChanges();
            return _mapper.Map<OrderDTO>(order);
        }
        public void UpdateStatus(int orderId, OrderStatus status)
        {
            Order order =  _db.Orders.Find(orderId);
            if (order == null) return;
            order.Status = status;
             _db.SaveChanges();   
        }
        public void setValidation(int orderId, bool isValid)
        {
            Order order = _db.Orders.Find(orderId);
            if (order == null) return;
            order.IsValid = isValid;
            _db.SaveChanges();
        }
    }
}
