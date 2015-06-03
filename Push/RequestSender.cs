using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ionic.Push
{
    /// <summary>
    /// Sends the request to the service
    /// </summary>
    public class RequestSender : IDisposable
    {
        /// <summary>
        /// The ionic push service url
        /// </summary>
        private const string SERVICE_URL = "https://push.ionic.io/api/v1/push";

        /// <summary>
        /// Configuration object which holds the information required by Ionic to authenticate the request
        /// </summary>
        private readonly PushAuthConfig pushConfig = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">Configuration object which holds the information required by Ionic to authenticate the request</param>
        /// <exception cref="ArgumentNullException">throws ArgumentNullException if <paramref name="config"/> is empty</exception>
        public RequestSender(PushAuthConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("PushConfig cannot be null");

            this.pushConfig = config;
        }

        /// <summary>
        /// Sends the request to the service
        /// </summary>
        /// <param name="requestData">The object holding the request data</param>
        /// <returns>object holding information about the request</returns>
        public ResponseObject SendRequest(RequestObject requestData)
        {
            if (requestData.Tokens == null || requestData.Tokens.Count == 0)
            {
                throw new ArgumentNullException("No tokens specified");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SERVICE_URL);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";

            string dataJson = JsonConvert.SerializeObject(requestData);
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataJson);

            request.ContentType = "application/json";
            request.ContentLength = dataBytes.Length;
            request.Headers["X-Ionic-Application-Id"] = pushConfig.ApplicationId;

            string authStr = pushConfig.PrivateKey + ":";
            request.Headers["Authorization"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(authStr));

            // HttpWebResponse throws 
            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(dataJson);
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                return ReadResponse(httpResponse);
            }
            catch (WebException we)
            {
                if (we.Status != WebExceptionStatus.ProtocolError)
                    throw we;

                return ReadResponse((HttpWebResponse)we.Response);
            }
        }

        /// <summary>
        /// R
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ResponseObject ReadResponse(HttpWebResponse response)
        {
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                ResponseData responseData = (ResponseData)JsonConvert.DeserializeObject(result, typeof(ResponseData));

                bool statusSuccess = response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
                bool isSuccess = responseData.Result == "queued" && statusSuccess;

                return new ResponseObject()
                {
                    Success = isSuccess,
                    StatusCode = response.StatusCode,
                    Data = responseData
                };
            }

        }

        public void Dispose()
        {

        }
    }
}