using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompression(this IServiceCollection services, Action<CompressionBuilder>? builder = null)
    {
        Guard.AgainstNull(services);

        var options = new CompressionBuilder(services);

        builder?.Invoke(options);

        services.TryAddSingleton<ICompressionService, CompressionService>();

        return services;
    }
}