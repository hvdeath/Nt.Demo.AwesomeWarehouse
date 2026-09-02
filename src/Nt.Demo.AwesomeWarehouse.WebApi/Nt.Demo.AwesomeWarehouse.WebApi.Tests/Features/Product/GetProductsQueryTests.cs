using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.Tests.Features.Product
{
    public class GetProductsQueryTests
    {
        private static WarehouseDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new WarehouseDbContext(options);
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsPagedProductsWithConvertedPrice()
        {
            // Arrange
            await using var db = CreateInMemoryContext("GetProducts_Paged");

            var now = DateTime.UtcNow;
            var products = new List<ProductEntity>
            {
                new ProductEntity { Name = "A", UnitPrice = 100, Quantity = 1, Weight = 1, Created = now.AddMinutes(-3), Modified = now.AddMinutes(-3), Version = new byte[8] },
                new ProductEntity { Name = "B", UnitPrice = 200, Quantity = 2, Weight = 2, Created = now.AddMinutes(-2), Modified = now.AddMinutes(-2), Version = new byte[8] },
                new ProductEntity { Name = "C", UnitPrice = 300, Quantity = 3, Weight = 3, Created = now.AddMinutes(-1), Modified = now.AddMinutes(-1), Version = new byte[8] }
            };

            db.Products.AddRange(products);
            await db.SaveChangesAsync(CancellationToken.None);
            var currency = new FakeCurrencyExchangeService(2m); // exchange rate HUF per EUR = 2 -> UnitPriceInEuros = UnitPrice / 2
            var query = new GetProductsQuery(db, currency);

            // Act
            var result = await query.ExecuteAsync(filter: null, pageNumber: 0, pageSize: 2, ct: CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            // Ordered by Modified desc -> C, B
            Assert.Equal("C", result[0].Name);
            Assert.Equal(300m / 2m, result[0].UnitPriceInEuros);
            Assert.Equal("B", result[1].Name);
            Assert.Equal(200m / 2m, result[1].UnitPriceInEuros);
        }

        [Fact]
        public async Task ExecuteAsync_ZeroExchangeRate_Throws()
        {
            // Arrange
            await using var db = CreateInMemoryContext("GetProducts_ZeroRate");
            db.Products.Add(new ProductEntity { Name = "X", UnitPrice = 50, Quantity = 1, Weight = 1, Created = DateTime.UtcNow, Modified = DateTime.UtcNow, Version = new byte[8] });
            await db.SaveChangesAsync(CancellationToken.None);

            var currency = new FakeCurrencyExchangeService(0m);

            // Act
            var query = new GetProductsQuery(db, currency);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => query.ExecuteAsync(null, 0, 10, CancellationToken.None));
        }

        [Fact]
        public async Task GetTotalCountAsync_ReturnsFilteredCount()
        {
            // Arrange
            await using var db = CreateInMemoryContext("GetProducts_Count");
            db.Products.AddRange(new[] {
                new ProductEntity { Name = "Apple", UnitPrice = 10, Quantity = 1, Weight = 1, Created = DateTime.UtcNow, Modified = DateTime.UtcNow, Version = new byte[8] },
                new ProductEntity { Name = "Banana", UnitPrice = 20, Quantity = 1, Weight = 1, Created = DateTime.UtcNow, Modified = DateTime.UtcNow, Version = new byte[8] },
                new ProductEntity { Name = "Apple Pie", UnitPrice = 30, Quantity = 1, Weight = 1, Created = DateTime.UtcNow, Modified = DateTime.UtcNow, Version = new byte[8] }
            });
            await db.SaveChangesAsync(CancellationToken.None);

            var currency = new FakeCurrencyExchangeService(1m);

            // Act
            var query = new GetProductsQuery(db, currency);

            // Assert
            var count = await query.GetTotalCountAsync("Apple", CancellationToken.None);
            Assert.Equal(2, count);
        }

        private class FakeCurrencyExchangeService : ICurrencyExchangeService
        {
            private readonly decimal rate;
            public FakeCurrencyExchangeService(decimal rate) => this.rate = rate;
            public Task<decimal> GetExhangeRateAsync(string fromCurrency, string toCurrency, CancellationToken ct)
                => Task.FromResult(rate);
        }
    }
}
