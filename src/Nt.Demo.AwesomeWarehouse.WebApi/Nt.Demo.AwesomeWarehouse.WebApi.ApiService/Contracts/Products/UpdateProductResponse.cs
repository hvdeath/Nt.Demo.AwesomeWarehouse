namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products
{
    public class UpdateProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public string? Description { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Version { get; set; }

    }
}
