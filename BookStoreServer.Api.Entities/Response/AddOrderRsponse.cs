using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.DTO;
using BookStoreServer.Entities.Events;

namespace BookStoreServer.Api.Entities.Response
{
    public class AddOrderRsponse
    {
        public OrderDTO order { get; set; }
        public OrderCreatedEvent  orderCreatedEvent { get; set; }
    }
}
