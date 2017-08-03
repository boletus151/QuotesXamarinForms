// -------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpService.cs" company="CodigoEdulis">
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
namespace QuotesXamarinForms.Interfaces
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using QuotesXamarinForms.Model.Enums;

    public interface IHttpService
    {
        Task<T> ExecuteQuery<T>(string url, HttpOperationMode mode, bool useTimeOut = false);

        Task<T> ExecuteQuery<T>(string url, HttpOperationMode mode, HttpContent content, bool useTimeOut = false);
    }
}