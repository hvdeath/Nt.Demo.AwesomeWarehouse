using FastEndpoints;
using Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Shared.Persistance;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Products.DeleteProduct
{
    public class DeleteProductEndpoint : EndpointWithoutRequest<object>
    {
        private readonly IDeleteProductOperation deleteProductOperation;

        public DeleteProductEndpoint(IDeleteProductOperation deleteProductOperation)
        {
            this.deleteProductOperation = deleteProductOperation;
        }

        public override void Configure()
        {
            Delete("/api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            int productId = Route<int>("id");
            var deleted = await deleteProductOperation.ExecuteAsync(productId, ct);

            if (!deleted)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            await Send.OkAsync();
        }
    }
}
