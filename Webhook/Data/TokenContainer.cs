using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Webhook
{
    /// <summary>
    /// A list of tokens containing the tokens of the devices which where registered by the user
    /// </summary>
    public class TokenContainer
    {
        /// <summary>
        /// Android devices of the user
        /// </summary>
        [JsonProperty(PropertyName = "android_tokens")]
        public List<string> AndroidTokens { get; set; }

        /// <summary>
        /// iOS devices of the user
        /// </summary>
        [JsonProperty(PropertyName = "ios_tokens")]
        public List<string> iOSTokens { get; set; }
    }
}
