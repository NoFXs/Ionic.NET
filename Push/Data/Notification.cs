using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    /// <summary>
    /// The notification container, which holds an alert text, and the data containers for the different operating systems
    /// </summary>
    /// <seealso cref="http://docs.ionic.io/v1.0/docs/push-from-scratch"/>
    public class Notification
    {
        /// <summary>
        /// The alert text
        /// </summary>
        [JsonProperty(PropertyName = "alert")]
        public string Alert { get; set; }

        /// <summary>
        /// The data container for the iOS devices
        /// </summary>
        [JsonProperty(PropertyName = "ios")]
        public iOSData iOS { get; set; }

        /// <summary>
        /// The data container for the android devices
        /// </summary>
        [JsonProperty(PropertyName = "android")]
        public AndroidData Android { get; set; }
    }
}
