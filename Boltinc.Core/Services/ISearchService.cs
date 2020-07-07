using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Boltinc.Core.Search;

namespace Boltinc.Core.Services
{
    public interface ISearchService
    {
        Task<SearchItemResponse> SearchAsync(SearchItemSpecification spec);
    }
}
