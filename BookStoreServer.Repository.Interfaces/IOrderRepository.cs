using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Api.Entities.Request;
using BookStoreServer.Api.Entities.Response;
using BookStoreServer.Entities.Events;

namespace BookStoreServer.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public List<OrderDTO> GetAll();
        public AddOrderRsponse AddOrder(OrderRequest request);
        public void UpdateStatus(int orderId, OrderStatus status);
        public void SetValidation(int orderId, bool isValid);
    }
}
