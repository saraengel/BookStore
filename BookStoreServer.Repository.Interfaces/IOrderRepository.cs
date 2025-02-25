using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;

namespace BookStoreServer.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<OrderDTO>> GetAllAsync();
        public Task<OrderDTO> AddOrderAsync(OrderRequest request);
        public Task UpdateStatusAsync(int orderId, OrderStatus status);
        public Task SetValidationAsync(int orderId, bool isValid);
    }
}
