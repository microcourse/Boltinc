using System;
using System.Collections.Generic;
using System.Text;
using Boltinc.Core.Domain;
using MediatR;
using SmartFormat;

namespace Boltinc.Core.Search
{
    public class SearchItemQuery : IRequest<IReadOnlyCollection<SearchItem>>
    {
        public string QueryText { get; set; }

        public SearchEngineType EngineType { get; set; }

        public int TopCount { get; set; } = 5;
    }
}
