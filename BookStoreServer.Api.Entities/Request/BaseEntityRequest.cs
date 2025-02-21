using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Request
{
    public class BaseEntityRequest<T>
    {
        public T Entity { get; set; }
    }
}
