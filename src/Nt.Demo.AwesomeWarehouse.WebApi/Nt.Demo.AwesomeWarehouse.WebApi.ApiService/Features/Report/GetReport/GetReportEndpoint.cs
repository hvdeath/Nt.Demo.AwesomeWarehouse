using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Report;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Mapper;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.GetReport
{
    public class GetReportEndpoint : EndpointWithoutRequest<GetReportResponse, GetReportMapper>
    {
        private readonly WarehouseDbContext dbContext;

        public GetReportEndpoint(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override void Configure()
        {
            Get("/api/reports");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new GetReportQuery(dbContext);
            var result = await query.ExecuteAsync(ct);
            await Send.OkAsync(Map.FromEntity(result));
        }

    }
}
