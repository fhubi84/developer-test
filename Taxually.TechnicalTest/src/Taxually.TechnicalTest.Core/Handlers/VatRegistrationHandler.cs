using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Core.Commands.Requests;
using Taxually.TechnicalTest.Core.Commands.Responses;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core.Handlers
{
    internal class VatRegistrationHandler : IRequestHandler<VatRegistrationRequest, VatRegistrationResponse>
    {
        private readonly IHttpClient _httpClient;
        private readonly IQueueClient _queueClient;

        private const string UkTaxUrl = "https://api.uktax.gov.uk";
        private const string CountryNotSupportedErrorMessage = "Country not supported";

        public VatRegistrationHandler(IHttpClient httpClient, IQueueClient queue)
        {
            _httpClient = httpClient;
            _queueClient = queue;
        }

        async Task<VatRegistrationResponse> IRequestHandler<VatRegistrationRequest, VatRegistrationResponse>.Handle(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            VatRegistrationResponse vatRegistrationResponse = new VatRegistrationResponse();

            switch (request.Country?.ToUpperInvariant())
            {
                case "GB":
                    // UK has an API to register for a VAT number
                    vatRegistrationResponse.Success = await HandleUKVatRequestAsync(request, cancellationToken);
                    break;
                case "FR":
                    // France requires an excel spreadsheet to be uploaded to register for a VAT number
                    vatRegistrationResponse.Success = await HandleFRVatRequestAsync(request, cancellationToken);
                    break;
                case "DE":
                    // Germany requires an XML document to be uploaded to register for a VAT number
                    vatRegistrationResponse.Success = await HandleDEVatRequestAsync(request, cancellationToken);
                    break;
                default:
                    vatRegistrationResponse.Success = false;
                    vatRegistrationResponse.ErrorMessage = CountryNotSupportedErrorMessage;
                    break;
            }

            return vatRegistrationResponse;
        }

        private async Task<bool> HandleUKVatRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            return await _httpClient.PostAsync(UkTaxUrl, request, cancellationToken);
        }

        private async Task<bool> HandleFRVatRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            // Queue file to be processed
            return await _queueClient.EnqueueAsync("vat-registration-csv", csv, cancellationToken);
        }

        private async Task<bool> HandleDEVatRequestAsync(VatRegistrationRequest request, CancellationToken cancellationToken)
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
