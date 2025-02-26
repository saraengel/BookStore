using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Entities.Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; set; }
        public string BooksName { get; set; }

        public OrderCreatedEvent(int orderId, string customerName, string customerEmail, string booksName)
        {
            OrderId = orderId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            BooksName = booksName;
        }
    }
}
