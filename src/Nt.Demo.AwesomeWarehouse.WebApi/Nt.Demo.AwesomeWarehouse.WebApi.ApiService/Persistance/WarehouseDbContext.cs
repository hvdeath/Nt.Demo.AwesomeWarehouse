using Microsoft.EntityFrameworkCore;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Persistance
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
           : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.Created)
                      .IsRequired();
                entity.Property(e => e.Version)
                      .IsRowVersion()
                      .IsRequired();

                entity.HasIndex(e => e.Name).HasDatabaseName("IX_Products_Name");
                entity.HasIndex(e => e.Modified).HasDatabaseName("IX_Products_Modified");

            });
        }
    }
}
