using System.Collections.Generic;
using System.Text;
using Boltinc.Core.Domain;

namespace Boltinc.Core.Search.Parrser
{
    public interface IParserFactory
    {
        ISearchItemParser Create(SearchEngineType engineType);
    }
}
