using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.Api.Entities.Response
{
    public class BaseResponse
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
        public ResponseStatus Status { get; set; }
        public BaseResponse()
        {
            Succeeded = true;
        }
        public BaseResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}
