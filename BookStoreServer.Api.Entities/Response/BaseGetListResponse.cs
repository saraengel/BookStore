using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Response
{
    public class BaseGetListResponse<T> : BaseResponse
    {
        public int TotalCount { get; set; }
        public List<T> Entities { get; set; }

      
    }
}
