using FastEndpoints;
using FluentValidation;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Contracts.Products;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Validators
{
    public class CreateProductRequestValidator : Validator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateProductRequestValidator : Validator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}
