using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public class CompressionBuilder
{
    public CompressionBuilder(IServiceCollection services)
    {
        Services = Guard.AgainstNull(services, nameof(services));
    }

    public IServiceCollection Services { get; }

    public CompressionBuilder AddNull()
    {
        Services.AddSingleton<ICompressionAlgorithm, NullCompressionAlgorithm>();

        return this;
    }
}