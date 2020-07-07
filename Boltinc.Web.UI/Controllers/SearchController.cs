using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boltinc.Core.Search;
using Boltinc.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boltinc.Web.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;


        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        // GET api/<SearchController>/5
        [HttpGet("{queryText}")]
        public async Task<SearchItemResponse> Get(string queryText)
        {
            var model = new SearchItemSpecification
            {
                QueryText = queryText
            };

            return await this.searchService.SearchAsync(model);
        }
    }
}
