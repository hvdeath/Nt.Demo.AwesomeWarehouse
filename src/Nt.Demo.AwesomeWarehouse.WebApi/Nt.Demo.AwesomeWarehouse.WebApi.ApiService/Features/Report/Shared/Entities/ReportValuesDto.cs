namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Report.Shared.Entities
{
    public class ReportValuesDto
    {
        public int TotalWeight { get; set; }
        public int TotalValue { get; set; }
        public int MostItemsProductId { get; set; }
        public int LargestWeightProductId { get; set; }
    }
}
