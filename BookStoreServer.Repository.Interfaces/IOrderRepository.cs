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
        public List<OrderDTO> GetAll();
        public OrderDTO AddOrder(OrderRequest request);
        public void UpdateStatus(int orderId, OrderStatus status);
        public void setValidation(int orderId, bool isValid);
    }
}
