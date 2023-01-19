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
using Taxually.TechnicalTest.Core.Factories;
using Taxually.TechnicalTest.Core.Interfaces;

namespace Taxually.TechnicalTest.Core.Handlers
{
    internal class VatRegistrationHandler : IRequestHandler<VatRegistrationRequest, VatRegistrationResponse>
    {
        private readonly IVatRequestProcessorFactory _factory;
        private const string CountryNotSupportedErrorMessage = "Country not supported";

        public VatRegistrationHandler(IVatRequestProcessorFactory factory)
        {
            _factory= factory;
        }

        public async Task<VatRegistrationResponse> Handle(VatRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            var vatRegistrationResponse = new VatRegistrationResponse();

            var processor = _factory.GetVatRequestProcessor(request.Country!);
            
            if (processor!= null) 
            {
                vatRegistrationResponse.Success = await processor.ProcessRequestAsync(request, cancellationToken);
            }
            else
            {
                vatRegistrationResponse.ErrorMessage = CountryNotSupportedErrorMessage;
            }

            return vatRegistrationResponse;
        }
    }
}
