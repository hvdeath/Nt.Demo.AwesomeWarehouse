using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductsEndpoint : Endpoint<FindProductsQueryRequest, FindProductsResponse, GetProductsMapper>
    {
        private readonly IGetProductsQuery getProductsQuery;

        public GetProductsEndpoint(IGetProductsQuery getProductsQuery)
        {
            this.getProductsQuery = getProductsQuery;
        }
        public override void Configure()
        {
            Get("/api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(FindProductsQueryRequest req, CancellationToken ct)
        {
            var entities = await getProductsQuery.ExecuteAsync(req.Filter, req.PageNumber, req.PageSize, ct);
            var count = await getProductsQuery.GetTotalCountAsync(req.Filter, ct);
            var result = entities.Select(Map.FromEntity);

            var response = new FindProductsResponse
            {
                Data = result.ToList(),
                Count = count
            };

            await Send.OkAsync(response);
        }
    }
}
