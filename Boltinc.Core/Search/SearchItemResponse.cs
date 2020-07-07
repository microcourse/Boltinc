using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Boltinc.Core.Domain;
using HtmlAgilityPack;

namespace Boltinc.Core.Search
{
    public class SearchItemResponse
    {
        public IReadOnlyCollection<SearchItem> Items { get; set; }
    }
}
