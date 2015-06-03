using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Webhook
{
    /// <summary>
    /// Data object holding the information when a client registration request is received by the webhook
    /// </summary>
    public class ClientRegisteredData
    {
        /// <summary>
        /// DateTime when the token was received
        /// </summary>
        [JsonProperty(PropertyName = "received")]
        public DateTime Received { get; set; }

        /// <summary>
        /// ID of the user, who registered his device
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Name of the user, who registered his device
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string UserName { get; set; }

        /// <summary>
        /// The application key of your app
        /// </summary>
        [JsonProperty(PropertyName = "app_id")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// The token list containing the device tokens of the user
        /// </summary>
        [JsonProperty(PropertyName = "_push")]
        public TokenContainer Tokens { get; set; }
    }
}
