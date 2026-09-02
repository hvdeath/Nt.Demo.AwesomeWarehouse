using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.GetReport
{
    public interface IGetReportQuery
    {
        Task<ReportValuesDto> ExecuteAsync(CancellationToken ct);
    }

    public class GetReportQuery : IGetReportQuery
    {
        private readonly WarehouseDbContext dbContext;

        public GetReportQuery(WarehouseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ReportValuesDto> ExecuteAsync(CancellationToken ct)
        {
            var reportValues = new ReportValuesDto
            {
                TotalValue = dbContext.Products.Sum(p => p.UnitPrice * p.Quantity),
                TotalWeight = dbContext.Products.Sum(p => p.Weight * p.Quantity),
                MostItemsProductId = dbContext.Products.OrderByDescending(p => p.Quantity).Select(p => p.Id).FirstOrDefault(),
                LargestWeightProductId = dbContext.Products.OrderByDescending(p => p.Weight).Select(p => p.Id).FirstOrDefault()
            };

            return reportValues;
        }
    }
}
