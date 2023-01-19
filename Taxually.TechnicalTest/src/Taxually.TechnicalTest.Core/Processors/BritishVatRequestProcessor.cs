using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Core.Commands.Requests;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core.Processors
{
    internal class BritishVatRequestProcessor : IVatRequestProcessor
    {
        private readonly IHttpClient _httpClient;
        private const string UkTaxUrl = "https://api.uktax.gov.uk";

        public BritishVatRequestProcessor(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ProcessRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await _httpClient.PostAsync(UkTaxUrl, request, cancellationToken);
        }
    }
}
