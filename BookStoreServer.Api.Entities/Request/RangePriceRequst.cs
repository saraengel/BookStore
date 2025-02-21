using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Request
{
    public class RangePriceRequest
    {
        public Decimal MinPrice { get; set; }
        public Decimal MaxPrice { get; set; }
    }
}
