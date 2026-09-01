using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.GetProducts;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductsQuery
    {
        private readonly WarehouseDbContext dbContext;
        private readonly ICurrencyExchangeService currencyExchangeService;

        public GetProductsQuery(WarehouseDbContext dbContext, ICurrencyExchangeService currencyExchangeService)
        {
            this.dbContext = dbContext;
            this.currencyExchangeService = currencyExchangeService;
        }

        public async Task<List<GetProductDto>> ExecuteAsync(string? filter, int pageNumber, int pageSize, CancellationToken ct)
        {
            var skip = pageNumber * pageSize;

            var products = await GetBaseQuery(filter)
                .OrderByDescending(p => p.Modified)
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct);

            var exchangeRate = await currencyExchangeService.GetExhangeRateAsync("EUR", "HUF", ct);

            if(exchangeRate == 0)
            {
                throw new InvalidOperationException("Exchange rate cannot be zero.");
            }

            return products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                UnitPriceInEuros = p.UnitPrice / exchangeRate,
                Description = p.Description,
                Weight = p.Weight,
                Quantity = p.Quantity,
                Created = p.Created,
                Modified = p.Modified,
                Version = p.Version
            }).ToList();
        }

        public async Task<int> GetTotalCountAsync(string? filter, CancellationToken ct)
        {
            return await GetBaseQuery(filter)
                .AsNoTracking()
                .CountAsync(ct);
        }

        private IQueryable<ProductEntity> GetBaseQuery(string? filter)
        {
            var query = dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.Name.Contains(filter));
            }
            return query;
        }
    }
}
