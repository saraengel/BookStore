using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsValid { get; set; }

    }
}
