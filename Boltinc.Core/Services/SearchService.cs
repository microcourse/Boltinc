using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boltinc.Core.Data;
using Boltinc.Core.Domain;
using Boltinc.Core.Search;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SmartFormat;

namespace Boltinc.Core.Services
{
    public class SearchService : ISearchService
    {
        private const string CachePattern = "SearchService.{0}";
        private readonly IMemoryCache _cache;
        private readonly IMediator _mediator;

        private readonly SearchEngineType[] _searchEngineTypes = new[]
        {
            SearchEngineType.Google,
            SearchEngineType.Bing
        };


        public SearchService(IMediator mediator, IMemoryCache cache)
        {
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<SearchItemResponse> SearchAsync(SearchItemSpecification spec)
        {
            spec = spec ?? throw new ArgumentNullException(nameof(spec));

            var cacheKey = Smart.Format(CachePattern, spec.QueryText);
            var result = _cache.GetOrCreateAsync(cacheKey, (entry => SearchInternalAsync(entry, spec)));

            return result;
        }

        private async Task<SearchItemResponse> SearchInternalAsync(ICacheEntry cacheEntry, SearchItemSpecification spec)
        {
            var result = await RunParallelSearchAsync(spec);

            await SaveSearchResultAsync(result);

            // Add to cache
            cacheEntry.Value = result;

            return result;
        }

        private async Task<SearchItemResponse> RunParallelSearchAsync(SearchItemSpecification spec)
        {
            var requests = this._searchEngineTypes
                .Select(x => new SearchItemQuery()
                {
                    QueryText = spec.QueryText,
                    EngineType = x
                }).ToArray();


            var tasks = requests
                .Select(x => _mediator.Send(x))
                .ToArray();


            await Task.WhenAll(tasks.Select(x => x as Task).ToArray());

            return new SearchItemResponse()
            {
                Items = tasks.SelectMany(x => x.Result).ToArray()
            };
        }

        private async Task SaveSearchResultAsync(SearchItemResponse result)
        {
            var bulkSaveCommand = new BulkSaveSearchItemCommand()
            {
                Items = result.Items
            };

            await _mediator.Send(bulkSaveCommand);
        }
    }
}