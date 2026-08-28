using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Entities;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.Shared.Mappers
{
    public class CreateProductMapper : Mapper<CreateProductRequest, CreateProductResponse, ProductEntity>
    {
        public override CreateProductResponse FromEntity(ProductEntity e)
        {
            return new CreateProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                UnitPrice = e.UnitPrice,
                Description = e.Description,
                Weight = e.Weight,
                Quantity = e.Quantity,
                Created = e.Created
            };
        }
        public override ProductEntity ToEntity(CreateProductRequest r)
        {
            return new ProductEntity
            {
                Name = r.Name,
                UnitPrice = r.UnitPrice,
                Description = r.Description,
                Weight = r.Weight,
                Quantity = r.Quantity
            };
        }
    }

    public class UpdateProductMapper : Mapper<UpdateProductRequest, UpdateProductResponse, ProductEntity>
    {
        public override UpdateProductResponse FromEntity(ProductEntity e)
        {
            return new UpdateProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                UnitPrice = e.UnitPrice,
                Description = e.Description,
                Weight = e.Weight,
                Quantity = e.Quantity,
                Created = e.Created,
                Version = Convert.ToBase64String(e.Version)
            };
        }
        public override ProductEntity ToEntity(UpdateProductRequest r)
        {
            return new ProductEntity
            {
                Name = r.Name,
                UnitPrice = r.UnitPrice,
                Description = r.Description,
                Weight = r.Weight,
                Quantity = r.Quantity,
                Version = Convert.FromBase64String(r.Version)
            };
        }
    }

    public class GetProductMapper : ResponseMapper<GetProductResponse, ProductEntity>
    {
        public override GetProductResponse FromEntity(ProductEntity e)
        {
            return new GetProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                UnitPrice = e.UnitPrice,
                Description = e.Description,
                Weight = e.Weight,
                Created = e.Created,
                Quantity = e.Quantity,
                Version = Convert.ToBase64String(e.Version)
            };
        }
    }
}
