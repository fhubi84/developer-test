using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Core.Commands.Requests;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core.Processors
{
    internal class GermanVatRequestProcessor : IVatRequestProcessor
    {
        private readonly IQueueClient _queueClient;

        public GermanVatRequestProcessor(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task<bool> ProcessRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            using var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, request);
            var xml = stringwriter.ToString();
            // Queue xml doc to be processed
            return await _queueClient.EnqueueAsync("vat-registration-xml", xml, cancellationToken);
        }
    }
}
