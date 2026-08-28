namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products
{
    public class FindProductsQueryRequest
    {
        public string? Filter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
