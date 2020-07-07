using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Boltinc.Core.Domain;
using HtmlAgilityPack;
using MediatR;
using SmartFormat;

namespace Boltinc.Core.Search
{
    public class SearchItemHttpHandler : IRequestHandler<SearchItemQuery, IReadOnlyCollection<SearchItem>>
    {
        private readonly IDictionary<SearchEngineType, SearchEngineConfig> _searchEngines;

        public SearchItemHttpHandler()
        {
            _searchEngines = GetSearchEngines().ToDictionary(k=> k.EngineType, v=>v);
        }

        public async Task<IReadOnlyCollection<SearchItem>> Handle(SearchItemQuery query, CancellationToken cancellationToken)
        {
            query = query ?? throw new ArgumentNullException(nameof(query));

            if (!_searchEngines.ContainsKey(query.EngineType))
            {
                throw new ArgumentOutOfRangeException(nameof(query.EngineType));
            }

            try
            {
                var config = _searchEngines[query.EngineType];
                var html = await LoadHtmlAsync(query, config, cancellationToken);
                var items = ParseHtml(html, config);
                var result = items.Take(query.TopCount).ToArray();

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw;
            }
        }

        private static async Task<string> LoadHtmlAsync(
            SearchItemQuery query, 
            SearchEngineConfig config,
            CancellationToken cancellationToken)
        {
            var url = Smart.Format(config.UriTemplate, query);

            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetStringAsync(url);

                return html;
            }
        }

        private static IEnumerable<SearchItem> ParseHtml(string html, SearchEngineConfig config)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var nodes = htmlDocument.DocumentNode.SelectNodes(config.TitleXpath);
                
            if(nodes == null)
            {
                return new SearchItem[0];
            }
            
            return nodes.Select(x => new SearchItem
            {
                Title = HtmlEntity.DeEntitize(x.InnerText ?? string.Empty),
                SearchEngine = config.EngineType
            });

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