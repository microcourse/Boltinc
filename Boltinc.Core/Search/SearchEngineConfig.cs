using Boltinc.Core.Domain;

namespace Boltinc.Core.Search
{
    public class SearchEngineConfig
    {
        public SearchEngineType EngineType { get; set; }
        public string UriTemplate { get; set; }
        public string TitleXpath { get; set; }
    }
}