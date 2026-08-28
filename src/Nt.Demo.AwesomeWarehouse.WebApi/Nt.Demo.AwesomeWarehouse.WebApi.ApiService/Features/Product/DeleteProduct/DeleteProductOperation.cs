using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.DeleteProduct
{
    public class DeleteProductOperation
    {
        private readonly WarehouseDbContext dbContext;

        public DeleteProductOperation(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExecuteAsync(int id, CancellationToken ct)
        {
            var entity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (entity == null)
                return false;

            dbContext.Products.Remove(entity);
            await dbContext.SaveChangesAsync(ct);
            return true;
        }
    }
}
