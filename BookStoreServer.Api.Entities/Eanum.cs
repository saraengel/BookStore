using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities
{
    public enum ResponseStatus
    {
        Succeeded = 1,
        NotSucceeded = 2,
        Unauthorized = 3,
        Created = 4,
    }
    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,

    }

}
