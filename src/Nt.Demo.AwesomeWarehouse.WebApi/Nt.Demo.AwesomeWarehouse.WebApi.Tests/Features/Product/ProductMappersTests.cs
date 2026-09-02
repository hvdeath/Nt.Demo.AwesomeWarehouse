using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.GetProducts;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers;
using System;
using Xunit;

namespace Nt.Demo.AwesomeWarehouse.WebApi.Tests.Features.Product
{
    public class ProductMappersTests
    {
        [Fact]
        public void CreateProductMapper_MapsEntityToResponse()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var entity = new ProductEntity
            {
                Id = 5,
                Name = "Test",
                UnitPrice = 123,
                Description = "desc",
                Weight = 10,
                Quantity = 2,
                Created = now,
                Modified = now,
                Version = new byte[] { 1, 2, 3 }
            };

            var mapper = new CreateProductMapper();

            // Act
            var resp = mapper.FromEntity(entity);

            // Assert
            Assert.Equal(entity.Id, resp.Id);
            Assert.Equal(entity.Name, resp.Name);
            Assert.Equal(entity.UnitPrice, resp.UnitPrice);
            Assert.Equal(entity.Description, resp.Description);
            Assert.Equal(entity.Weight, resp.Weight);
            Assert.Equal(entity.Quantity, resp.Quantity);
            Assert.Equal(entity.Created, resp.Created);
            Assert.Equal(entity.Modified, resp.Modified);
        }

        [Fact]
        public void UpdateProductMapper_MapsVersionToBase64()
        {
            // Arrange
            var entity = new ProductEntity
            {
                Id = 7,
                Name = "U",
                UnitPrice = 10,
                Description = "d",
                Weight = 1,
                Quantity = 1,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Version = new byte[] { 10, 20, 30 }
            };

            var mapper = new UpdateProductMapper();

            // Act
            var resp = mapper.FromEntity(entity);

            // Assert
            Assert.Equal(Convert.ToBase64String(entity.Version), resp.Version);
            Assert.Equal(entity.Id, resp.Id);
        }

        [Fact]
        public void GetProductMapper_MapsEntityToGetProductResponse()
        {
            // Arrange
            var entity = new ProductEntity
            {
                Id = 9,
                Name = "G",
                UnitPrice = 55,
                Description = "gd",
                Weight = 5,
                Quantity = 3,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Version = new byte[] { 4, 5, 6 }
            };

            var mapper = new GetProductMapper();

            // Act
            var resp = mapper.FromEntity(entity);

            // Assert
            Assert.Equal(entity.Id, resp.Id);
            Assert.Equal(entity.Name, resp.Name);
            Assert.Equal(entity.UnitPrice, resp.UnitPrice);
            Assert.Equal(Convert.ToBase64String(entity.Version), resp.Version);
        }

        [Fact]
        public void GetProductsMapper_MapsDtoToGetProductResponse()
        {
            // Arrange
            var dto = new GetProductDto
            {
                Id = 11,
                Name = "D",
                UnitPrice = 200,
                UnitPriceInEuros = 100m,
                Description = "desc",
                Weight = 2,
                Quantity = 4,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Version = new byte[] { 7, 8, 9 }
            };

            var mapper = new GetProductsMapper();

            // Act
            var resp = mapper.FromEntity(dto);

            // Assert
            Assert.Equal(dto.Id, resp.Id);
            Assert.Equal(dto.Name, resp.Name);
            Assert.Equal(dto.UnitPrice, resp.UnitPrice);
            Assert.Equal(dto.UnitPriceInEuros, resp.UnitPriceInEuros);
            Assert.Equal(Convert.ToBase64String(dto.Version), resp.Version);
        }
    }
}
