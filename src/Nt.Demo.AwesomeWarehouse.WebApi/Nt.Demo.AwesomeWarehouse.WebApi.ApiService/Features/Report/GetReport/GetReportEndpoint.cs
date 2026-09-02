using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Report;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Mapper;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.GetReport
{
    public class GetReportEndpoint : EndpointWithoutRequest<GetReportResponse, GetReportMapper>
    {
        private readonly IGetReportQuery getReportQuery;

        public GetReportEndpoint(IGetReportQuery getReportQuery)
        {
            this.getReportQuery = getReportQuery;
        }
        public override void Configure()
        {
            Get("/api/reports");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await getReportQuery.ExecuteAsync(ct);
            await Send.OkAsync(Map.FromEntity(result));
        }

    }
}
