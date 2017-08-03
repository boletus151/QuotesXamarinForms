// -------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpService.cs" company="CodigoEdulis">
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
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using QuotesXamarinForms.Interfaces;
    using QuotesXamarinForms.Model.Enums;

    public class HttpService : IHttpService
    {
        public async Task<T> ExecuteQuery<T>(string url, HttpOperationMode mode, bool useTimeOut = false)
        {
            var stringContent = new StringContent(string.Empty);

            return await this.ExecuteQuery<T>(url, mode, stringContent, useTimeOut);
        }

        public async Task<T> ExecuteQuery<T>(string url, HttpOperationMode mode, HttpContent content, bool useTimeOut = false)
        {
            var taskResult = default(T);
            switch(mode)
            {
                case HttpOperationMode.GET:
                    taskResult = await this.GetAsync<T>(url, useTimeOut);
                    break;

                case HttpOperationMode.POST:
                    taskResult = await this.PostAsync<T>(url, content, useTimeOut);
                    break;

                case HttpOperationMode.PUT:
                    taskResult = await this.PutAsync<T>(url, content, useTimeOut);
                    break;

                case HttpOperationMode.DELETE:
                    taskResult = await this.DeleteAsync<T>(url, useTimeOut);
                    break;
            }

            return taskResult;
        }

        private HttpClient CreateClient(bool useTimeOut = false)
        {
            try
            {
                var client = new HttpClient();

                // todo
                if(string.IsNullOrEmpty("token"))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", string.Empty);
                }
                else
                {
                    var finalToken = $"Bearer token";
                    client.DefaultRequestHeaders.Add("Authorization", finalToken);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                }

                if(useTimeOut)
                {
                    client.Timeout = new TimeSpan(0, 2, 30);
                }

                return client;
            }
            catch(Exception ex)
            {
                // UserWarningException.RegisterException("CreateHttpClientEx", ex);
            }

            return new HttpClient();
        }

        private async Task<T> DeleteAsync<T>(string url, bool useTimeOut = false)
        {
            using(var client = this.CreateClient(useTimeOut))
            {
                var result = await client.DeleteAsync(url);

                if(result != null)
                {
                    var parsedResult = await this.ProcessJson<T>(result.Content);
                    return parsedResult;
                }
            }

            return default(T);
        }

        private async Task<T> GetAsync<T>(string url, bool useTimeOut = false)
        {
            using(var client = this.CreateClient(useTimeOut))
            {
                var result = await client.GetAsync(url);

                if(result != null)
                {
                    var parsedResult = await this.ProcessJson<T>(result.Content);
                    return parsedResult;
                }
            }

            return default(T);
        }

        private async Task<T> PostAsync<T>(string url, HttpContent content, bool useTimeOut = false)
        {
            using(var client = this.CreateClient(useTimeOut))
            {
                var result = await client.PostAsync(url, content);

                if(result != null)
                {
                    var parsedResult = await this.ProcessJson<T>(result.Content);
                    return parsedResult;
                }
            }

            return default(T);
        }

        private async Task<T> ProcessJson<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            var deserializedData = JsonConvert.DeserializeObject<T>(json);

            return deserializedData;
        }

        private async Task<T> PutAsync<T>(string url, HttpContent content, bool useTimeOut = false)
        {
            using(var client = this.CreateClient(useTimeOut))
            {
                var result = await client.PutAsync(url, content);

                if(result != null)
                {
                    var parsedResult = await this.ProcessJson<T>(result.Content);
                    return parsedResult;
                }
            }

            return default(T);
        }
    }
}