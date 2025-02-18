using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.DTO
{
    public class BookDTO
    {
      
        public int id { get; set; }
        public string? title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public Decimal price { get; set; }
    }
}
