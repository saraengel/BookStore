using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Response
{
    public class BaseGetEntityResponse<T>:BaseResponse
    {
        public T Entity { get; set; }

        public BaseGetEntityResponse()
        {
        }
    }
}
