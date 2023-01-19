using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Taxually.TechnicalTest.Core.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTechnicalTestCoreAssemblyForMediatR(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection serviceCollection, Assembly assembly)
        {
            AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(item => serviceCollection.AddSingleton(item.InterfaceType, item.ValidatorType));

            return serviceCollection;
        }
    }
}
