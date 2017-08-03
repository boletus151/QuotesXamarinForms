// -------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiProvider.cs" company="CodigoEdulis">
//    Código Edulis 2017
//    http://www.codigoedulis.es
//  </copyright>
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//     
//     You are free to:
// 
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//     
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//     
//     Under the following terms:
//     
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//  
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace QuotesXamarinForms.Implementations
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using QuotesXamarinForms.Interfaces;
    using QuotesXamarinForms.Model;

    public class WebApiProvider : IWebApiProvider
    {
        private static readonly string BaseUrl = @"http://quoteswebapiv1.azurewebsites.net";

        private static readonly string AddQuoteUrl = $"{BaseUrl}/api/quotes";

        private static readonly string DeleteQuoteUrl = $"{BaseUrl}/api/quotes";

        private static readonly string GetQuotesUrl = $"{BaseUrl}/api/quotes";

        private readonly IHttpService httpService;

        public WebApiProvider(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public Task<string> AddQuoteAsync(Quote newQuote)
        {
            var json = JsonConvert.SerializeObject(newQuote);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return this.httpService.ExecuteQuery<string>(AddQuoteUrl, Model.Enums.HttpOperationMode.POST, stringContent);
        }

        public Task<bool> DeleteQuoteAsync(int id)
        {
            return this.httpService.ExecuteQuery<bool>(DeleteQuoteUrl, Model.Enums.HttpOperationMode.DELETE);
        }

        public Task<Quote> GetQuoteByIdAsync(int id)
        {
            var url = $"{GetQuotesUrl}/{id}";
            return this.httpService.ExecuteQuery<Quote>(url, Model.Enums.HttpOperationMode.GET);
        }

        public Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return this.httpService.ExecuteQuery<IEnumerable<Quote>>(GetQuotesUrl, Model.Enums.HttpOperationMode.GET);
        }
    }
}