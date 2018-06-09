using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TestingHttpClient.Testable.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {

        public FakeHttpMessageHandler(HttpStatusCode statusCode, HttpContent httpContent)
            : this(statusCode, httpContent, new Dictionary<String, String>())
        {

        }


        public FakeHttpMessageHandler(HttpStatusCode statusCode, HttpContent httpContent, IDictionary<String, String> headers)
        {
            this._statusCode = statusCode;
            this._httpContent = httpContent;
            this._headers = headers;
        }



        private HttpStatusCode _statusCode;
        private HttpContent _httpContent;
        private IDictionary<String, String> _headers;


        public static readonly MediaTypeHeaderValue APPLICATION_JSON = new MediaTypeHeaderValue("application/json");

        public static readonly MediaTypeHeaderValue APPLICATION_XML = new MediaTypeHeaderValue("application/xml");

        public static readonly MediaTypeHeaderValue TEXT_PLAIN = new MediaTypeHeaderValue("text/plain");



        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = this._statusCode,
                Content = _httpContent,
                RequestMessage = request,                
            };
            _headers.ToList().ForEach(h => response.Headers.Add(h.Key, h.Value));
            
            return Task.FromResult(response);
        }




        public static FakeHttpMessageHandler CreateResponseHandler(HttpStatusCode statusCode, String result, MediaTypeHeaderValue contentType)
        {            
            var content = new StringContent(result);
            content.Headers.ContentType = contentType;

            return new FakeHttpMessageHandler(statusCode, content);
        }


        /// <summary>
        /// Creates a FakeHttpMessageHandler that will serialize the provided object to JSON and use it as the content of the response
        /// </summary>
        /// <typeparam name="T">The Type of the object to be serialized for the response</typeparam>
        /// <param name="statusCode">The HttpStatusCode value of the status code to use for the response</param>
        /// <param name="result">The object to be serialized and used as the content response</param>
        /// <returns>A FakeHttpMessageHandler object</returns>
        public static FakeHttpMessageHandler CreateJsonResponseHandler<T>(HttpStatusCode statusCode, T result)
        {
            var json = JsonConvert.SerializeObject(result);
            var content = new StringContent(json);
            content.Headers.ContentType = APPLICATION_JSON;
            
            return new FakeHttpMessageHandler(statusCode, content);
        }


        public static FakeHttpMessageHandler CreateBinaryResponseHandler<T>(HttpStatusCode statusCode, byte[] binaryContent)
        {            
            var content = new ByteArrayContent(binaryContent);

            return new FakeHttpMessageHandler(statusCode, content);
        }

    }
}
