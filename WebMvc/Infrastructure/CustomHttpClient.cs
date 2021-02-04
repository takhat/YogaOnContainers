using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        // ASP.Net space already has a browser HttpClient
        private readonly HttpClient _client;

        //making an instance of the browser in the constructor.
        //we don't need to inject from Startup.cs because we need it 
        //only for this class
        public CustomHttpClient()
        {
            _client = new HttpClient();
        }
        public async Task<string> GetStringAsync(string uri, 
            string authorization = null, 
            string authorizationMethod = "Bearer")
        {
            //equiv. of selecting Get and providing a Uri in Postman:
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //equiv. of clicking send button in Postman:
            var response = await _client.SendAsync(requestMessage);
            //response contains many things but we need only the content:
            return await response.Content.ReadAsStringAsync();
        }
    }
}
