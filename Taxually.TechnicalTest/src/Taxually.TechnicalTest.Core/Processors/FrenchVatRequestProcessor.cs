using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Core.Commands.Requests;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core.Processors
{
    internal class FrenchVatRequestProcessor : IVatRequestProcessor
    {
        private readonly IQueueClient _queueClient;

        public FrenchVatRequestProcessor(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task<bool> ProcessRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            // Queue file to be processed
            return await _queueClient.EnqueueAsync("vat-registration-csv", csv, cancellationToken);
        }
    }
}
