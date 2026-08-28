using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.DeleteProduct
{
    public class DeleteProductEndpoint : EndpointWithoutRequest<object>
    {
        private readonly WarehouseDbContext dbContext;

        public DeleteProductEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Configure()
        {
            Delete("/api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var operation = new DeleteProductOperation(dbContext);
            int productId = Route<int>("id");
            var deleted = await operation.ExecuteAsync(productId, ct);

            if (!deleted)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            await Send.OkAsync();
        }
    }
}
