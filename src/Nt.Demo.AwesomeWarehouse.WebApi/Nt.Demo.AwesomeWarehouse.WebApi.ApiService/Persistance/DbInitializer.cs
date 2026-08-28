using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(WarehouseDbContext context)
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new List<ProductEntity>();            

            for (int i = 0; i < 200; i++)
            {
                products.Add(new ProductEntity { Name = $"Product {i + 5}", Description = $"Description {i + 5}", UnitPrice = 1099 + i, Weight = 100 + i, Quantity = 1, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
