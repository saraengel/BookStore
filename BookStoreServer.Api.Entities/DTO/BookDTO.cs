using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.DTO
{
    public class BookDTO
    {
      
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public Decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public int Amount { get; set; }

    }
}
