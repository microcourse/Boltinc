using System;
using System.Collections.Generic;
using System.Linq;
using Boltinc.Core.Domain;

namespace Boltinc.Core.Search.Parrser
{
    public class ParserFactory : IParserFactory
    {
        private static readonly IDictionary<SearchEngineType, SearchEngineConfig> SearchEngines;

        static ParserFactory()
        {
            SearchEngines = GetSearchEngines().ToDictionary(k => k.EngineType, v => v);
        }

        public ISearchItemParser Create(SearchEngineType engineType)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<SearchEngineConfig> GetSearchEngines()
        {
            yield return new SearchEngineConfig()
            {
                EngineType = SearchEngineType.Google,
                UriTemplate = "https://www.google.com/search?q={QueryText}",
                TitleXpath = "//a/h3"
            };

            yield return new SearchEngineConfig()
            {
                EngineType = SearchEngineType.Bing,
                UriTemplate = "https://www.bing.com/search?q={QueryText}",
                TitleXpath = "//h2/a/span"
            };
        }
    }
}