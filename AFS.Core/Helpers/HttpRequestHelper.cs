using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AFS.Core.Helpers
{
    public class HttpRequestHelper
    {
        static readonly HttpClient _client;
        static HttpRequestHelper()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        public async Task<Response> PostAsync<Request, Response>(
            string url,
            Request input,
            string token = null)
        {
            return await CreateRequest<Response>(url, HttpMethod.Post, input, token);
        }

        public async Task<Response> GetAsync<Response>(
            string url,
            string token = null)
        {
            return await CreateRequest<Response>(url, HttpMethod.Get, token);
        }

        #region [ -- Private helper methods -- ]
        async Task<Response> CreateRequest<Response>(
            string url,
            HttpMethod method,
            string token)
        {
            return await CreateRequestMessage(url, method, token, async (msg) =>
            {
                return await GetResult<Response>(msg);
            });
        }

        async Task<Response> CreateRequest<Response>(
            string url,
            HttpMethod method,
            object input,
            string token)
        {
            return await CreateRequestMessage(url, method, token, async (msg) =>
            {
                using (var content = new StringContent(JObject.FromObject(input).ToString()))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    msg.Content = content;
                    return await GetResult<Response>(msg);
                }
            });
        }

        async Task<Response> CreateRequestMessage<Response>(
            string url,
            HttpMethod method,
            string token,
            Func<HttpRequestMessage, Task<Response>> functor)
        {
            using (var msg = new HttpRequestMessage())
            {
                msg.RequestUri = new Uri(url);
                msg.Method = method;
                if (token != null)
                    msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await functor(msg);
            }
        }

        async Task<Response> GetResult<Response>(HttpRequestMessage msg)
        {

            using (var response = await _client.SendAsync(msg))
            {
                using (var content = response.Content)
                {
                    var responseContent = await content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(responseContent);

                    if (typeof(IConvertible).IsAssignableFrom(typeof(Response)))
                        return (Response)Convert.ChangeType(responseContent, typeof(Response));

                    try
                    {
                        var responseContentByte = await content.ReadAsByteArrayAsync();
                        return (Response)Convert.ChangeType(responseContentByte, typeof(Response));
                    }
                    catch (Exception)
                    {
                        return JToken.Parse(responseContent).ToObject<Response>();
                    }
                }
            }
        }
        #endregion
    }
}
