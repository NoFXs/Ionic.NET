using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    public class ResponseObject
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ResponseData Data { get; set; }
    }
}
