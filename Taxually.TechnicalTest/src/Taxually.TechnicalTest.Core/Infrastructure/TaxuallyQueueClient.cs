using System.Threading;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core
{
    public class TaxuallyQueueClient : IQueueClient
    {
        public Task<bool> EnqueueAsync<TPayload>(string queueName, TPayload payload, CancellationToken cancellationToken)
        {
            // Code to send to message queue removed for brevity
            return Task.FromResult(true);
        }
    }
}
