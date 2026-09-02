using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public interface IGetProductByIdQuery
    {
        Task<ProductEntity?> ExecuteAsync(int id, CancellationToken ct);
    }

    public class GetProductByIdQuery : IGetProductByIdQuery
    {
        private readonly WarehouseDbContext dbContext;

        public GetProductByIdQuery(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductEntity?> ExecuteAsync(int id, CancellationToken ct)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        }

    }
}
