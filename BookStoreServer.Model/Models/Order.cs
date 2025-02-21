using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities;

namespace BookStoreServer.Model.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("User")]
        public int UserId {  get; set; }
        public int Amount { get; set; }
        public OrderStatus Status {  get; set; }
        public bool IsValid { get; set; }
        public Order()
        {
            Status = OrderStatus.Pending;
            IsValid = false;
        }
    }
}
