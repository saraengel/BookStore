using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Common.Events
{
    public class OrderStatusChangedEvent : EventArgs
    {
        public int OrderId { get; }
        public string Status { get; }

        public OrderStatusChangedEvent(int orderId, string status)
        {
            OrderId = orderId;
            Status = status;
        }
    }
}
