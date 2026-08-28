using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductsQuery
    {
        private readonly WarehouseDbContext dbContext;

        public GetProductsQuery(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProductEntity>> ExecuteAsync(string? filter, int pageNumber, int pageSize, CancellationToken ct)
        {
            var skip = pageNumber * pageSize;

            return await GetBaseQuery(filter)
                .OrderByDescending(p => p.Modified)
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct);
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
