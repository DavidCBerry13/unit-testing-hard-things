using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingHttpClient.Testable.Tests
{

    /// <summary>
    /// Class used to simulate HTTP Timeouts for testing
    /// </summary>
    public class TimeoutMessageHandler : HttpMessageHandler
    {


        /// <summary>
        /// Immediately throws an OperationCanceledException to simulate an HTTP call timing out
        /// </summary>
        /// <param name="request">The HTTP Request to be processed (Not used in this handler)</param>
        /// <param name="cancellationToken">The CancellationToken passed with the request</param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new OperationCanceledException("The operation has timed out");
        }
    }
}
