namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services
{
    public interface ICurrencyExchangeService
    {
        Task<decimal> GetExhangeRateAsync(string fromCurrency, string toCurrency, CancellationToken ct);
    }
}