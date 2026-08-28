using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.CreateProduct
{
    public class CreateProductEndpoint : Endpoint<CreateProductRequest, CreateProductResponse, CreateProductMapper>
    {
        private readonly WarehouseDbContext dbContext;

        public CreateProductEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override void Configure()
        {
            Post("/api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
        {
            var operation = new CreateProductOperation(dbContext);
            var entity = await operation.ExecuteAsync(Map.ToEntity(req), ct);
            await Send.OkAsync(Map.FromEntity(entity));
        }
    }
}
