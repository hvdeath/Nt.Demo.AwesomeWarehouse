using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductsEndpoint : Endpoint<FindProductsQueryRequest, FindProductsResponse, GetProductMapper>
    {
        private readonly WarehouseDbContext dbContext;

        public GetProductsEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override void Configure()
        {
            Get("/api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(FindProductsQueryRequest req, CancellationToken ct)
        {
            var query = new GetProductsQuery(dbContext);
            var entities = await query.ExecuteAsync(req.Filter, req.PageNumber, req.PageSize, ct);
            var count = await query.GetTotalCountAsync(req.Filter, ct);
            var result = entities.Select(e => Map.FromEntity(e));

            var response = new FindProductsResponse
            {
                Data = result.ToList(),
                Count = count
            };

            await Send.OkAsync(response);
        }
    }
}
