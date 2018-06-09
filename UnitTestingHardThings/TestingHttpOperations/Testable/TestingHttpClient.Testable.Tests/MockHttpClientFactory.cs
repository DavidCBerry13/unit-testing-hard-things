using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestingHttpClient.Testable.Tests
{
    public class FakeHttpClientFactory
    {

        public const String CONTENT_TYPE_APPLICATION_JSON = "application/json";


        public static HttpClient CreateFakeHttpClientForStringContent(HttpStatusCode httpStatusCode, String contentType, String content)
        {
            var contentTypeHeader = MediaTypeHeaderValue.Parse(contentType);
            return CreateFakeHttpClientForStringContent(httpStatusCode, contentTypeHeader, content);
        }


        public static HttpClient CreateFakeHttpClientForStringContent(HttpStatusCode httpStatusCode, MediaTypeHeaderValue contentType, String content)
        {
            var stringContent = new StringContent(content);
            stringContent.Headers.ContentType = contentType;

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(httpStatusCode, stringContent);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            httpClient.BaseAddress = new Uri("http://localhost/Test");

            return httpClient;
        }


        public static HttpClient CreateFakeHttpClientForJsonStringContent(HttpStatusCode statusCode, String jsonString)
        {

            var content = new StringContent(jsonString);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(CONTENT_TYPE_APPLICATION_JSON);

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(statusCode, content);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            httpClient.BaseAddress = new Uri("http://localhost/Test");

            return httpClient;
        }


        public static HttpClient CreateFakeHttpClientForJsonContent<T>(HttpStatusCode statusCode, T content)
        {
            var json = JsonConvert.SerializeObject(content);
            var jsonContent = new StringContent(json);
            jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse(CONTENT_TYPE_APPLICATION_JSON);

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(statusCode, jsonContent);

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            httpClient.BaseAddress = new Uri("http://localhost/Test");

            return httpClient;
        }



        public static HttpClient CreateFakeHttpClientForTimeout()
        {
            var fakeHttpMessageHandler = new TimeoutMessageHandler();

            var httpClient = new HttpClient(fakeHttpMessageHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(1);
            httpClient.BaseAddress = new Uri("http://localhost/Test");

            return httpClient;
        }


    }
}
