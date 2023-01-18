using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Taxually.TechnicalTest.Core.Interfaces
{
    public interface IQueueClient
    {
        Task<bool> EnqueueAsync<TPayload>(string queueName, TPayload payload, CancellationToken cancellationToken);
    }
}
