namespace Nt.Demo.AwesomeWarehouse.WebApi.ApiService.Features.Product.Shared.Excetions
{
    public class ConcurrencyConflictException : Exception
    {
        public ConcurrencyConflictException(string type, Exception ex) : base($"Updating {type} failed because of concurrency conflict.", ex)
        {
        }

        public ConcurrencyConflictException(string type) : base($"Updating {type} failed because of concurrency conflict.")
        {
        }
    }
}
