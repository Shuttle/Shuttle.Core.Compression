using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public class CompressionBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; } = Guard.AgainstNull(services, nameof(services));

    public CompressionBuilder AddNull()
    {
        Services.AddSingleton<ICompressionAlgorithm, NullCompressionAlgorithm>();

        return this;
    }
}