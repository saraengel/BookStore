﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.DTO
{
    public class BookDTO
    {
      
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public Decimal price { get; set; }
        public int Amount { get; set; }

    }
}
