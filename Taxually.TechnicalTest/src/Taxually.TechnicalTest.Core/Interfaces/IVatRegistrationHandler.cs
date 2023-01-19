using System.Threading;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Core.Commands.Requests;

namespace Taxually.TechnicalTest.Core.Interfaces
{
    public interface IVatRequestProcessor
    {
        Task<bool> ProcessRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken);
    }
}
