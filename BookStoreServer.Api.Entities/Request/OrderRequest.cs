using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Request
{
    public class OrderRequest
    {
        public int  UserId {  get; set; }   
        public string BookTitle { get; set; }
        public int Amount { get; set; }
    }
}
