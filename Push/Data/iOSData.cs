using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    /// <summary>
    /// The data container for the iOS devices
    /// </summary>
    /// <seealso cref="http://docs.ionic.io/v1.0/docs/push-from-scratch"/>
    public class iOSData
    {
        [JsonProperty(PropertyName = "badge")]
        public int Badge { get; set; }

        [JsonProperty(PropertyName = "sound")]
        public string Sound { get; set;}

        [JsonProperty(PropertyName = "expiry")]
        public long Expiry { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "contentAvailable")]
        public bool ContentAvailable { get; set; }
        
        [JsonProperty(PropertyName = "payload")]
        public KeyValuePair<string, string> Payload { get; set; }
    }
}
