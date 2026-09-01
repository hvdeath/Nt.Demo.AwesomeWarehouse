using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.GetProducts
{
    public class GetProductsEndpoint : Endpoint<FindProductsQueryRequest, FindProductsResponse, GetProductsMapper>
    {
        private readonly WarehouseDbContext dbContext;
        private readonly ICurrencyExchangeService currencyExchangeService;

        public GetProductsEndpoint(WarehouseDbContext dbContext, ICurrencyExchangeService currencyExchangeService)
        {
            this.dbContext = dbContext;
            this.currencyExchangeService = currencyExchangeService;
        }
        public override void Configure()
        {
            Get("/api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(FindProductsQueryRequest req, CancellationToken ct)
        {
            var query = new GetProductsQuery(dbContext, currencyExchangeService);
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
