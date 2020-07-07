using System.Collections.Generic;
using System.Text;
using Boltinc.Core.Domain;

namespace Boltinc.Core.Search.Parrser
{
    public interface ISearchItemParser
    {
        public IReadOnlyCollection<SearchItem> Parse(string htmlString);
    }
}
