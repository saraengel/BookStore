using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ConcurrentBag<OrderDTO> _orders = new ConcurrentBag<OrderDTO>();


        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }
      
        public BaseGetListResponse<OrderDTO> GetAll()
        {
            List <OrderDTO> orders = _orderRepository.GetAll();
            return new BaseGetListResponse<OrderDTO>() { Entities = orders };
        }
        public void AddOrder(OrderRequest request)
        {
           OrderDTO order  = _orderRepository.AddOrder(request);
            _orders.Add(order);
        }
        public async Task ProcessOrdersAsync()
        {
            Task validationTask = Task.Run(() => ValidateOrders());
            Task processingTask = Task.Run(() => ProcessValidOrders());
            await Task.WhenAll(validationTask, processingTask);
            Console.WriteLine("All orders processed");
        }
        public void ValidateOrders()
        {
            while (!_orders.IsEmpty)
            {
                if (_orders.TryTake(out OrderDTO order))
                {
                  BookDTO bookDTO =  _bookRepository.GetBook(order.BookId);
                    if (bookDTO == null) return;
                    if (bookDTO.Amount >= order.Amount)
                    {
                        _orderRepository.setValidation(order.Id, true);
                    }
                }
            }
        }
        public  void ProcessValidOrders()
        {
            Console.WriteLine("Processing Orders...");
        
            while (!_orders.IsEmpty)
            {
                foreach(OrderDTO order in _orders )
                {
                    if (order.IsValid)
                    {
                        Console.WriteLine($"Processing Order Id: {order.Id}");
                        _orderRepository.UpdateStatus(order.Id, OrderStatus.Processing);// update status in DB

                        Task.Delay(2000);  //  סימולציה של עיבוד
                        _orderRepository.UpdateStatus(order.Id, OrderStatus.Completed);// update status in DB
                    }
                }
            }
        }

    }
}
