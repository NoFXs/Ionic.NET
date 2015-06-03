using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    public class RequestObject
    {
        [JsonProperty(PropertyName = "tokens")]
        public List<string> Tokens { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public Notification Notification { get; set; }
    }
}
