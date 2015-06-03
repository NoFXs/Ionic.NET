using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Webhook
{
    /// <summary>
    /// Data object holding the information when a client unregistration request is received by the webhook
    /// </summary>
    public class ClientUnregisteredData
    {
        /// <summary>
        /// True if the Device is unregistered
        /// </summary>
        [JsonProperty(PropertyName = "unregister")]
        public bool Unregister { get; set; }

        /// <summary>
        /// The application key of your app
        /// </summary>
        [JsonProperty(PropertyName = "app_id")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// DateTime when the token was received
        /// </summary>
        [JsonProperty(PropertyName = "received")]
        public DateTime Received { get; set; }

        /// <summary>
        /// The token list containing the device tokens of the user
        /// </summary>
        [JsonProperty(PropertyName = "_push")]
        public TokenContainer Tokens { get; set; }
    }
}
