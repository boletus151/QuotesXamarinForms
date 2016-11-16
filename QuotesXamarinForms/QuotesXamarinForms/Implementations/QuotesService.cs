using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuotesXamarinForms.Interfaces;
using QuotesXamarinForms.Model;

namespace QuotesXamarinForms.Implementations
{
    public class QuotesService : IQuotesService
    {
        private const string BasicUrl = @"http://sharedwebapiexample.azurewebsites.net/api/";
        private const string GetAllQuotesUrl = @"quotes";
        private readonly HttpClient client;

        public QuotesService()
        {
            this.client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        public async Task<IList<Quote>> GetAllQuotesAsync()
        {
            var uri = new Uri(BasicUrl + GetAllQuotesUrl);
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Quote>>(content);
            return list;
        }
    }
}