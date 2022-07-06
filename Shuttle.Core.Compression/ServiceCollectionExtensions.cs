using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompression(this IServiceCollection services,
            Action<CompressionOptions> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var options = new CompressionOptions(services);

            builder?.Invoke(options);

            services.TryAddSingleton<ICompressionService, CompressionService>();

            return services;
        }

    }
}