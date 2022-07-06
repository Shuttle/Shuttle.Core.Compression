using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class CompressionOptions
    {
        public CompressionOptions(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            Services = services;
        }

        public IServiceCollection Services { get; }

        public CompressionOptions AddGzip()
        {
            Services.AddSingleton<ICompressionAlgorithm, GZipCompressionAlgorithm>();

            return this;
        }

        public CompressionOptions AddDeflate()
        {
            Services.AddSingleton<ICompressionAlgorithm, DeflateCompressionAlgorithm>();

            return this;
        }
    }
}