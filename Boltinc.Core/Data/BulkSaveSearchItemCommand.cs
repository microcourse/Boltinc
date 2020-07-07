using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Data;
using Boltinc.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boltinc.Core.Data
{
    public class BulkSaveSearchItemCommand : IRequest
    {
        public IReadOnlyCollection<SearchItem> Items { get; set; } = new SearchItem[0];
    }

    public class DataContextHandler : AsyncRequestHandler<BulkSaveSearchItemCommand>
    {
        private readonly DbContext dbContext;

        public DataContextHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override Task Handle(BulkSaveSearchItemCommand request, CancellationToken cancellationToken)
        {
            this.dbContext.AddRange(request.Items ?? new SearchItem[0]);

            return this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
