using System.Text.Json;

namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly HttpClient httpClient;

        public CurrencyExchangeService(HttpClient httpClient)
        {
            //todo use client factory
            this.httpClient = httpClient;
        }

        public async Task<decimal> GetExhangeRateAsync(string fromCurrency, string toCurrency, CancellationToken ct)
        {
            string uri = $"v2/rates?quotes={fromCurrency},{toCurrency}";
            var responseString = await httpClient.GetStringAsync(uri, ct);
            
            var rates = JsonSerializer.Deserialize<CurrencyExchangeRateDto[]>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<CurrencyExchangeRateDto>();

            var fromRate = rates.FirstOrDefault(r => r.Quote.Equals(toCurrency, StringComparison.OrdinalIgnoreCase));
            if (fromRate == null)
            {
                throw new NotSupportedException($"Exchange rate from {fromCurrency} to {toCurrency} is not supported.");
            }

            return fromRate.Rate;
        }
    }
}
