namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public string? Description { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
    }
}
