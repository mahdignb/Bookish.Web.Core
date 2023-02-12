using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Response
{
    public class StandardResponse<T>
    {
        public string ResponseText { get; set; }
        public string ResponseStatus { get; set; }
        public T Data { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Failed
    }
}
