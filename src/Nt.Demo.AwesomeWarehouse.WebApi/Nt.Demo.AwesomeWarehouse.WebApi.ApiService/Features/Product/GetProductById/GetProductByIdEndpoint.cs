using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductByIdEndpoint : EndpointWithoutRequest<GetProductResponse, GetProductMapper>
    {
        private readonly IGetProductByIdQuery getProductByIdQuery;

        public GetProductByIdEndpoint(IGetProductByIdQuery getProductByIdQuery)
        {
            this.getProductByIdQuery = getProductByIdQuery;
        }
        public override void Configure()
        {
            Get("/api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            int productId = Route<int>("id");
            var entity = await getProductByIdQuery.ExecuteAsync(productId, ct);

            if (entity == null)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            await Send.OkAsync(Map.FromEntity(entity));
        }

    }
}
