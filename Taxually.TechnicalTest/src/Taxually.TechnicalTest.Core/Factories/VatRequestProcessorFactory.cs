using Microsoft.Extensions.DependencyInjection;
using System;
using Taxually.TechnicalTest.Core.Interfaces;
using Taxually.TechnicalTest.Core.Processors;

namespace Taxually.TechnicalTest.Core.Factories
{
    internal class VatRequestProcessorFactory : IVatRequestProcessorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public VatRequestProcessorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;      
        }

        public IVatRequestProcessor? GetVatRequestProcessor(string country)
        {
            return (country?.ToUpperInvariant()) switch
            {
                "GB" => serviceProvider.GetService<BritishVatRequestProcessor>(),
                "FR" => serviceProvider.GetService<FrenchVatRequestProcessor>(),
                "DE" => serviceProvider.GetService<GermanVatRequestProcessor>(),
                _ => null,
            };
        }
    }
}
