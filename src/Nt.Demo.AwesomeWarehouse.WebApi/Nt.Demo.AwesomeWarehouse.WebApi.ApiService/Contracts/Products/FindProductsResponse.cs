namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products
{
    public class FindProductsResponse
    {
        public List<GetProductResponse> Data { get; set; }
        public int Count { get; set; }
    }
}
