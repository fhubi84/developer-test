using System.Threading;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core
{
    public class TaxuallyHttpClient : IHttpClient
    {
        public Task<bool> PostAsync<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
        {
            // Actual HTTP call removed for purposes of this exercise
            return Task.FromResult(true);
        }
    }
}
