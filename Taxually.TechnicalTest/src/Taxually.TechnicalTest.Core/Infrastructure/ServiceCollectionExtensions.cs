using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Taxually.TechnicalTest.Core.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTechnicalTestCoreAssemblyForMediatR(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
