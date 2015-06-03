using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Webhook
{
    /// <summary>
    /// Container for the token if an android token gets invalid
    /// </summary>
    public class AndroidTokenInvalidData
    {
        /// <summary>
        /// The invalid token
        /// </summary>
        [JsonProperty(PropertyName = "android_token")]
        public string Token { get; set; }

        /// <summary>
        /// true if the token is invalid
        /// </summary>
        [JsonProperty(PropertyName = "token_invalid")]
        public bool TokenInvalid { get; set; }
    }
}
