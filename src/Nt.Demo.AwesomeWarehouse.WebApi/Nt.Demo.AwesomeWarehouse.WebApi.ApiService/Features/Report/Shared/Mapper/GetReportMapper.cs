using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Report;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Entities;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Mapper
{
    public class GetReportMapper : ResponseMapper<GetReportResponse, ReportValuesDto>
    {
        public override GetReportResponse FromEntity(ReportValuesDto e)
        {
            return new GetReportResponse
            {
                TotalWeight = e.TotalWeight,
                TotalValue = e.TotalValue,
                MostItemsProductId = e.MostItemsProductId,
                LargestWeightProductId = e.LargestWeightProductId
            };
        }
    }
}
