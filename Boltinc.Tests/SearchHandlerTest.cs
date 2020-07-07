using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Data;
using Boltinc.Core.Domain;
using Boltinc.Core.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Boltinc.Tests
{
    public class AppTest
    {
        [Fact]
        public async Task GoogleSearchTest()
        {
            var request = new SearchItemQuery()
            {
                QueryText = "MediaR",
                EngineType = SearchEngineType.Google
                
            };

            var handler = new SearchItemHttpHandler();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.Count < 5);
        }

        [Fact]
        public async Task BingSearchTest()
        {
            var request = new SearchItemQuery()
            {
                QueryText = "MediaR",
                EngineType = SearchEngineType.Bing
            };

            var handler = new SearchItemHttpHandler();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.Count < 5);
        }


        [Fact]
        public async Task DbContextQueryTests()
        {
            var context = CreateDbContext();
            var items = context.Set<SearchItem>()
                .AsNoTracking().ToArray();



            context.Set<SearchItem>().Add(new SearchItem() {Title = "dddddd"});

            await context.SaveChangesAsync();

            Assert.NotNull(context);
        }



        private static AppDbContext CreateDbContext()
        {
           var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:AppDbContext"];

            var buider = new DbContextOptionsBuilder();

            buider.UseSqlServer(connectionString);

            var dbContext = new AppDbContext(buider.Options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
