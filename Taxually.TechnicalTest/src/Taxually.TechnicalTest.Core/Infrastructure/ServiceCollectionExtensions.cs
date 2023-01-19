using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Taxually.TechnicalTest.Core.Factories;
using Taxually.TechnicalTest.Core.Interfaces;
using Taxually.TechnicalTest.Core.Processors;

namespace Taxually.TechnicalTest.Core.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTechnicalTestCoreServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddScoped<VatRequestProcessorFactory>()
                .AddScoped<BritishVatRequestProcessor>()
                .AddScoped<FrenchVatRequestProcessor>()
                .AddScoped<GermanVatRequestProcessor>();
        }

        private static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection serviceCollection, Assembly assembly)
        {
            AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(item => serviceCollection.AddSingleton(item.InterfaceType, item.ValidatorType));

            return serviceCollection;
        }
    }
}
