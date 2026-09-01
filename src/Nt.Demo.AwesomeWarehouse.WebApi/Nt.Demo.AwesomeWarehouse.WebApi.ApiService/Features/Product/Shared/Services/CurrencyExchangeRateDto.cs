namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Services
{
    // result example: [{"date":"2026-09-01","base":"EUR","quote":"EUR","rate":1.0},{"date":"2026-09-01","base":"EUR","quote":"HUF","rate":365.58}]
    public class CurrencyExchangeRateDto
    {
        public string Date { get; set; }
        public string Base { get; set; }
        public string Quote { get; set; }
        public decimal Rate { get; set; }
    }
}
