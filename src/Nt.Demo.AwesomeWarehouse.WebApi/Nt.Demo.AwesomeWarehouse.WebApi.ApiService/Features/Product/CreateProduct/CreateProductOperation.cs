using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.CreateProduct
{
    public class CreateProductOperation
    {
        private readonly WarehouseDbContext dbContext;

        public CreateProductOperation(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductEntity> ExecuteAsync(ProductEntity entity, CancellationToken ct)
        {
            entity.Created = entity.Modified = DateTime.UtcNow;
            
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync(ct);

            return entity;
        }
    }
}
