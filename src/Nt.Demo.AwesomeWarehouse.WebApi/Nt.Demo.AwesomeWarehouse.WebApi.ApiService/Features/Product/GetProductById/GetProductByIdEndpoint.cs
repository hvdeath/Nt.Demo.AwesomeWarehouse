using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductByIdEndpoint : EndpointWithoutRequest<GetProductResponse, GetProductMapper>
    {
        private readonly WarehouseDbContext dbContext;

        public GetProductByIdEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override void Configure()
        {
            Get("/api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new GetProductByIdQuery(dbContext);
            int productId = Route<int>("id");
            var entity = await query.ExecuteAsync(productId, ct);

            if (entity == null)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            await Send.OkAsync(Map.FromEntity(entity));
        }

    }
}
