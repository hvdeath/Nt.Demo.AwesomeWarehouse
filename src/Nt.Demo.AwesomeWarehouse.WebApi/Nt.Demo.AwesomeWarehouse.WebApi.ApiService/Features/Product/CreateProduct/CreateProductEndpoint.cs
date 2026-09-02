using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.CreateProduct
{
    public class CreateProductEndpoint : Endpoint<CreateProductRequest, CreateProductResponse, CreateProductMapper>
    {
        private readonly ICreateProductOperation createProductOperation;

        public CreateProductEndpoint(ICreateProductOperation createProductOperation)
        {
            this.createProductOperation = createProductOperation;
        }
        public override void Configure()
        {
            Post("/api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
        {
            var entity = await createProductOperation.ExecuteAsync(Map.ToEntity(req), ct);
            await Send.OkAsync(Map.FromEntity(entity));
        }
    }
}
