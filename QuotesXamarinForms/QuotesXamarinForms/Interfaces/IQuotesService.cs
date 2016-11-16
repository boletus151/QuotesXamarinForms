using System.Collections.Generic;
using System.Threading.Tasks;
using QuotesXamarinForms.Model;

namespace QuotesXamarinForms.Interfaces
{
    public interface IQuotesService
    {
        Task<IList<Quote>> GetAllQuotesAsync();
    }
}