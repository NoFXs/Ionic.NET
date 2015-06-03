using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace Ionic.Webhook
{
    /// <summary>
    /// Reads the Webhook-Requests, parses them and returns the parsed object
    /// </summary>
    /// <example>
    ///     // Unfortunately you have to cast the object returned by this class, because we can't know which type Ionic will send to the webhook
    ///     // But here's a example how to use this class from within the ASP MVC controller class.
    ///     var requestReader = new Ionic.Webhook.RequestReader();
    ///     var result = requestReader.Read(Request.InputStream);
    ///
    ///     switch (result.RequestType)
    ///     {
    ///         case RequestType.Registered:
    ///             ClientRegisteredData registerData = (ClientRegisteredData)result.Data;
    ///             RegisterToken(registerData);
    ///             break;
    ///         case RequestType.Unregistered:
    ///             ClientUnregisteredData unregisterData = (ClientUnregisteredData)result.Data;
    ///             UnregisterToken(unregisterData);
    ///             break;
    ///         case RequestType.InvalidAndroid:
    ///             AndroidTokenInvalidData androidTokenInvalidData = (AndroidTokenInvalidData)result.Data;
    ///             MarkAndroidTokenAsInvalid(androidTokenInvalidData);
    ///             break;
    ///         case RequestType.InvalidiOS:
    ///             iOSTokenInvalidData iOSTokenInvalidData = (iOSTokenInvalidData)result.Data;
    ///             MarkiOSTokenAsInvalid(iOSTokenInvalidData);
    ///             break;
    ///     }
    /// </example>
    public class RequestReader
    {
        /// <summary>
        /// Reads the InputStream of the request and analyzes it.
        /// </summary>
        /// <param name="inputStream">The Request.InputStream</param>
        /// <returns>The type of the object, sent by Ionic Webhook and the parsed object, which has to be casted</returns>
        /// <example>
        /// For example code please have a look at the example-Tag of the RequestReader class comment
        /// </example>
        public ParseResult Read(Stream inputStream)
        {
            var reader = new StreamReader(inputStream);
            var inputString = reader.ReadToEnd();

            // Now try all request types we know so far. Any should fit.
            var registeredData = TryParseResponse<ClientRegisteredData>(inputString, RequestType.Registered);
            if (registeredData.Success)
            {
                return new ParseResult()
                {
                    RequestType = RequestType.Registered,
                    Data = registeredData.Data
                };
            }
            var unregisteredData = TryParseResponse<ClientUnregisteredData>(inputString, RequestType.Unregistered);
            if (unregisteredData.Success)
            {

                return new ParseResult()
                {
                    RequestType = RequestType.Unregistered,
                    Data = registeredData.Data
                };
            }
            var androidInvalidData = TryParseResponse<AndroidTokenInvalidData>(inputString, RequestType.InvalidAndroid);
            if (androidInvalidData.Success)
            {

                return new ParseResult()
                {
                    RequestType = RequestType.InvalidAndroid,
                    Data = registeredData.Data
                };
            }
            var iOSInvalidData = TryParseResponse<iOSTokenInvalidData>(inputString, RequestType.InvalidiOS);
            if (iOSInvalidData.Success)
            {

                return new ParseResult()
                {
                    RequestType = RequestType.InvalidiOS,
                    Data = registeredData.Data
                };
            }

            throw new NotImplementedException("Unknown request content");
        }

        /// <summary>
        /// Tries to parse the json request
        /// </summary>
        /// <typeparam name="T">Type of the parsed object</typeparam>
        /// <param name="jsonRequest">The request string</param>
        /// <param name="requestType">Type of the request</param>
        /// <returns>The result which contains if parsing was successfull and if parsing was successfull then the parsed object</returns>
        TryParseResult<T> TryParseResponse<T>(string jsonRequest, RequestType requestType)
        {
            // Using a schema because just parsing and catching exceptions can be slow
            var jsonSchema = GetSchema(requestType);

            JsonSchema schema = JsonSchema.Parse(jsonSchema);
            JObject jsonObject = JObject.Parse(jsonRequest);

            // If you get errors, look for the messages in the errorMessages list. 
            // I left it here, so you can analyze the errors
            IList<string> errorMessages = new List<string>();
            if (!jsonObject.IsValid(schema, out errorMessages))
            {
                return new TryParseResult<T>() { Success = false };
            }

            // Try to deserialize:
            try
            {
                T data = (T)JsonConvert.DeserializeObject(jsonRequest, typeof(T));
                return new TryParseResult<T>() { Success = true, Data = data };
            }
            catch
            {
                // The JSON response seemed to be a deserializable object, but failed to deserialize.
                // This case should not occur...
                return new TryParseResult<T>() { Success = false };
            }
        }

        /// <summary>
        /// Returns the right schema for the request type
        /// </summary>
        /// <param name="requestType">The type which corresponding schema should be returned</param>
        /// <returns>the schema</returns>
        private string GetSchema(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Registered:
                    return @"
                            {
                              'title': 'ClientRegisteredData',
                              'type': 'object',
                              'properties': {
                                'received': {
                                  'required': true,
                                  'type': 'string'
                                },
                                'user_id': {
                                  'required': true,
                                  'type': 'integer'
                                },
                                'name': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                'app_id': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                'message': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                '_push': {
                                  'required': true,
                                  'type': [
                                    'object',
                                    'null'
                                  ],
                                  'properties': {
                                    'android_tokens': {
                                      'required': false,
                                      'type': [
                                        'array',
                                        'null'
                                      ],
                                      'items': {
                                        'type': [
                                          'string',
                                          'null'
                                        ]
                                      }
                                    },
                                    'ios_tokens': {
                                      'required': false,
                                      'type': [
                                        'array',
                                        'null'
                                      ],
                                      'items': {
                                        'type': [
                                          'string',
                                          'null'
                                        ]
                                      }
                                    }
                                  }
                                }
                              }
                            }";
                case RequestType.Unregistered:
                    return @"
                            {
                              'title': 'ClientUnregisteredData',
                              'type': 'object',
                              'properties': {
                                'unregister': {
                                  'required': true,
                                  'type': 'boolean'
                                },
                                'app_id': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                'received': {
                                  'required': true,
                                  'type': 'string'
                                },
                                '_push': {
                                  'required': true,
                                  'type': [
                                    'object',
                                    'null'
                                  ],
                                  'properties': {
                                    'android_tokens': {
                                      'required': true,
                                      'type': [
                                        'array',
                                        'null'
                                      ],
                                      'items': {
                                        'type': [
                                          'string',
                                          'null'
                                        ]
                                      }
                                    },
                                    'ios_tokens': {
                                      'required': true,
                                      'type': [
                                        'array',
                                        'null'
                                      ],
                                      'items': {
                                        'type': [
                                          'string',
                                          'null'
                                        ]
                                      }
                                    }
                                  }
                                }
                              }
                            }";
                case RequestType.InvalidAndroid:
                    return @"
                            {
                              'title': 'AndroidTokenInvalidData',
                              'type': 'object',
                              'properties': {
                                'android_token': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                'token_invalid': {
                                  'required': true,
                                  'type': 'boolean'
                                }
                              }
                            }
                            ";
                case RequestType.InvalidiOS:
                    return @"
                            {
                              'title': 'iOSTokenInvalidData',
                              'type': 'object',
                              'properties': {
                                'ios_token': {
                                  'required': true,
                                  'type': [
                                    'string',
                                    'null'
                                  ]
                                },
                                'token_invalid': {
                                  'required': true,
                                  'type': 'boolean'
                                }
                              }
                            }
                            ";
                default:
                    throw new NotImplementedException();
            }
        }


        private class TryParseResult<T>
        {
            public bool Success { get; set; }
            public T Data { get; set; }
        }
    }
}
