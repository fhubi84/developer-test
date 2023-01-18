using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Taxually.TechnicalTest.Core.Interfaces
{
    public interface IHttpClient
    {
        Task<bool> PostAsync<TRequest>(string url, TRequest request, CancellationToken cancellationToken);
    }
}
