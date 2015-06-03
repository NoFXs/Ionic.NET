using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Webhook
{
    public class ParseResult
    {
        public RequestType RequestType { get; set; }

        public object Data { get; set; }
    }
}
