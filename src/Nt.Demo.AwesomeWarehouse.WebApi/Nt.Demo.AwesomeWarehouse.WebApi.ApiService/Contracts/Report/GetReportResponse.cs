namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Report
{
    public class GetReportResponse
    {
        public int TotalWeight { get; set; }
        public int TotalValue { get; set; }
        public int MostItemsProductId { get; set; }
        public int LargestWeightProductId { get; set; }
    }
}
