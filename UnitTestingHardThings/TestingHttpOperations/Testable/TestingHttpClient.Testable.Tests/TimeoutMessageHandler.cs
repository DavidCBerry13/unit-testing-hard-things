using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingHttpClient.Testable.Tests
{
    public class TimeoutMessageHandler : HttpMessageHandler
    {



        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new OperationCanceledException("The operation has timed out");
        }
    }
}
