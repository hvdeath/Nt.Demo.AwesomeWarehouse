using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Excetions;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.UpdateProduct
{
    public class UpdateProductOperation
    {
        private readonly WarehouseDbContext dbContext;

        public UpdateProductOperation(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductEntity?> ExecuteAsync(ProductEntity entity, CancellationToken ct)
        {
            var existingEntity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == entity.Id, ct);
            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Name = entity.Name;
            existingEntity.UnitPrice = entity.UnitPrice;
            existingEntity.Description = entity.Description;
            existingEntity.Weight = entity.Weight;
            existingEntity.Quantity = entity.Quantity;
            existingEntity.Modified = DateTime.UtcNow;

            dbContext.Entry(existingEntity).Property(nameof(ProductEntity.Version)).OriginalValue = entity.Version;

            try
            {
                await dbContext.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyConflictException(entity.GetType().Name, e);
            }

            return existingEntity;
        }
    }
}
