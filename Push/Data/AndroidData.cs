using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    /// <summary>
    /// The data container for the android devices
    /// </summary>
    /// <seealso cref="http://docs.ionic.io/v1.0/docs/push-from-scratch"/>
    public class AndroidData
    {
        [JsonProperty(PropertyName = "collapseKey")]
        public string CollapseKey { get; set; }

        [JsonProperty(PropertyName = "delayWhileIdle")]
        public bool DelayWhileIdle { get; set; }

        [JsonProperty(PropertyName = "timeToLive")]
        public int TTL { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public KeyValuePair<string, string> Payload { get; set; }
    }
}
