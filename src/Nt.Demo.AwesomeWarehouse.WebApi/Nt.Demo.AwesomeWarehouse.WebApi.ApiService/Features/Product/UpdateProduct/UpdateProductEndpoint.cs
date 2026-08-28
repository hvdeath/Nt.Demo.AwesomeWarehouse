using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Excetions;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.UpdateProduct
{
    public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, UpdateProductResponse, UpdateProductMapper>
    {
        private readonly WarehouseDbContext dbContext;

        public UpdateProductEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Configure()
        {
            Put("/api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            try
            {
                var operation = new UpdateProductOperation(dbContext);

                var productEntity = Map.ToEntity(req);
                int productId = Route<int>("id");
                productEntity.Id = productId;

                var entity = await operation.ExecuteAsync(productEntity, ct);

                if (entity == null)
                {
                    await Send.NotFoundAsync(ct);
                    return;
                }

                await Send.OkAsync(Map.FromEntity(entity));
            }
            catch (ConcurrencyConflictException)
            {
                await Send.StatusCodeAsync(409, ct);
            }

        }
    }
}
