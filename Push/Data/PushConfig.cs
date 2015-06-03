using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    /// <summary>
    /// Configuration object which holds the information required by Ionic to authenticate the request
    /// </summary>
    public class PushAuthConfig
    {
        /// <summary>
        /// The private respectively secret key of your app
        /// </summary>
        /// <seealso cref="http://docs.ionic.io/v1.0/docs/io-api-keys"/>
        public string PrivateKey { get; set; }

        /// <summary>
        /// The application key of your app
        /// </summary>
        /// <seealso cref="http://docs.ionic.io/v1.0/docs/io-api-keys"/>
        public string ApplicationId { get; set; }
    }
}
