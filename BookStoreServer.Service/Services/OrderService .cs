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
using BookStoreServer.CommonServices;
using BookStoreServer.Entities.Events;
using BookStoreServer.Model.Models;
using BookStoreServer.Repository;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IHubContext<StoreHub> _orderHub;
        private readonly ILogger<OrderService> _logger;
        private readonly EventAggregator _eventAggregator;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, IHubContext<StoreHub> orderHub, ILogger<OrderService> logger, EventAggregator eventAggregator)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _orderHub = orderHub;
            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        public async Task<BaseGetListResponse<OrderDTO>> GetAllAsync()
        {
            try
            {
                List<OrderDTO> orders = await _orderRepository.GetAllAsync();
                return new BaseGetListResponse<OrderDTO> { Entities = orders, Succeeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all orders.");
                return new BaseGetListResponse<OrderDTO> { Succeeded = false, ErrorMessage = "An error occurred while retrieving orders." };
            }
        }

        public async Task<BaseGetEntityResponse<OrderDTO>> AddOrderAsync(OrderRequest request)
        {
            try
            {
                // Validate request
                if (request == null || request.Amount <= 0)
                {
                    return new BaseGetEntityResponse<OrderDTO> { Succeeded = false, ErrorMessage = "Invalid order request." };
                }
                var result = await _orderRepository.AddOrderAsync(request);
                //await _orderHub.Clients.All.SendAsync("OrderValidated", order.Id);
                _eventAggregator.Publish(result.orderCreatedEvent);
                return new BaseGetEntityResponse<OrderDTO> { Entity = result.order, Succeeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding an order.");
                return new BaseGetEntityResponse<OrderDTO> { Succeeded = false, ErrorMessage = "An error occurred while adding the order." };
            }
        }
    }
}
