using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Result
{
   public class ResponseForRequest
    {
        public bool Successful {get; set; }
        public List<string> Errors { get; set; }
    }
}
