using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class CompressionBuilder
    {
        public CompressionBuilder(IServiceCollection services)
        {
            Services = Guard.AgainstNull(services, nameof(services));
        }

        public IServiceCollection Services { get; }

        public CompressionBuilder AddGzip()
        {
            Services.AddSingleton<ICompressionAlgorithm, GZipCompressionAlgorithm>();

            return this;
        }

        public CompressionBuilder AddDeflate()
        {
            Services.AddSingleton<ICompressionAlgorithm, DeflateCompressionAlgorithm>();

            return this;
        }

        public CompressionBuilder AddNull()
        {
            Services.AddSingleton<ICompressionAlgorithm, NullCompressionAlgorithm>();

            return this;
        }
    }
}